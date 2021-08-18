using System;
using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;

namespace EF_Task_Standard.Scope
{
    /// <summary>
    /// Interface to define scope of atomic business operation.<para/>
    /// In terms of EF it means that all repositories will get the same instance of <see cref="DbContext"/>>.<para/>
    /// It allows to build complex queries with joins and at the same time be fully abstracted away from concrete DbContext.<para/>
    /// Should be used primarily on Business Logic layer, in services and wrap set of repository operations.<para/>
    /// Typically we call it at the beginning of the method and dispose at the end of it.<para/>
    /// Another use case is usage in tests. It may help to create scope and use calls to repositories in order to fetch data for integration testing.
    /// </summary>
    /// <typeparam name="T">database context type</typeparam>
    public interface IScopeContextCreator<in T>
        where T : DbContext
    {
        /// <summary>
        /// Creates new scope and <see cref="DbContext"/> instance along with it.<para/>
        /// For convenience this call could be wrapped in another class for concrete version of <see cref="T"/>
        /// </summary>
        /// <param name="contextBuilder">delegate for <see cref="DbContext"/> creation</param>
        /// <returns>instance of business context</returns>
        IDisposable CreateReadScope(Func<T> contextBuilder);
    }

    /// <summary>
    /// Interface to define accessor to current available and valid instance of <see cref="DbContext"/>.<para/>
    /// This should be called usually in Data Access Layer, in repositories for examples.<para/>
    /// Typically we call it at the beginning of each repository method, get <see cref="DbContext"/> instance and work with it.<para/>
    /// We don't have to dispose context after that, because overlying context responsible for it.<para/>
    /// For write operations you may call SaveChanges() or may not. It depends on your architectural design.<para/>
    /// If you decide not to call SaveChanges(), then overlying context responsible for making this call.<para/>
    /// Also in this scenario you won't get intermediate transaction data. For example Id of newly created entity.<para/>
    /// In that case you have to implement intermediate save call again in parent context.
    /// </summary>
    /// <typeparam name="T">database context type</typeparam>
    public interface IContextLocator<out T>
        where T : DbContext
    {
        /// <summary>
        /// Locates current <see cref="DbContext"/> instance available for scope defined.<para/>
        /// In order to ensure atomicity of operation this method should be called for each individual operation.<para/>
        /// You could reuse instance of IContextLocator without any problems, but be careful with result of this operation.<para/>
        /// Do not spread context from this between different public methods of your DAL classes.
        /// </summary>
        /// <returns>instance of <see cref="T"/></returns>
        T GetCurrentDbContext();
    }

    /// <summary>
    /// This class implements both IScopeContextCreator and IContextLocator.<para/>
    /// Share instance of <see cref="DbContext"/> between consumers in defined scope.<para/>
    /// Ensures following behavior: thread safety DbContext passing, safe async DbContext passing, disposal of context instance when scope disposed.<para/>
    /// In order to avoid unexpected behavior this class should be registered as singleton. If this is not meant to be used as interface
    /// </summary>
    /// <typeparam name="T">inheritor of <see cref="DbContext"/></typeparam>
    internal class DbScopeCollection<T> : IScopeContextCreator<T>, IContextLocator<T>
        where T : DbContext
    {
        /// <summary>
        /// We need this for identification purposes in our call context. It will act as a key to our shared data
        /// </summary>
        private static readonly string _identityName = Guid.NewGuid().ToString("N");

        /// <summary>
        /// Simple wrapper class in order to ensure:<para/>
        /// 1) Immutability. We must ensure non-mutated environment for our storage. This is good for multi-threaded scenarios.<para/>
        /// 2) Cross AppDomain intercommunication. This object could be passed between different processes and in remoting scenarios.
        /// </summary>
        private sealed class Wrapper : MarshalByRefObject
        {
            public ImmutableStack<T> Value { get; set; }
        }

        /// <inheritdoc cref="IContextLocator{T}"/>
        public T GetCurrentDbContext() => CurrentContexts.Peek();

        /// <inheritdoc cref="IScopeContextCreator{T}"/>
        public IDisposable CreateReadScope(Func<T> contextBuilder)
        {
            CurrentContexts = CurrentContexts.Push(contextBuilder.Invoke());
            return new PopWhenDisposed();
        }

        /// <summary>
        /// Actual data storage of shared database context. Usage of <see cref="CallContext"/><para/>
        /// ensures that correct instance will be passed and used between different threads and async calls.
        /// </summary>
        private static ImmutableStack<T> CurrentContexts
        {
            get => AsyncLocalStore.GetData(_identityName) is Wrapper context ? context.Value : ImmutableStack.Create<T>();
            set => AsyncLocalStore.SetData(_identityName, new Wrapper {Value = value});
        }

        /// <summary>
        /// Called at the of the business transaction.<para/>
        /// We need to dispose <see cref="DbContext"/> first and then remove it from collection.
        /// </summary>
        private static void Pop()
        {
            CurrentContexts.Peek().Dispose();
            CurrentContexts = CurrentContexts.Pop();
        }

        /// <summary>
        /// We need to return something disposable to scope owner. Actual type and access to this class does not matter.<para/>
        /// Important part of the scope is disposal, which states end of particular business operation.
        /// </summary>
        private sealed class PopWhenDisposed : IDisposable
        {
            private bool disposed;

            public void Dispose()
            {
                if (disposed)
                {
                    return;
                }

                Pop();
                disposed = true;
            }
        }
    }
}
