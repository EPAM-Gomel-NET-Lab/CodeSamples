using System;

namespace EF_Task_Standard.Scope
{
    public interface IScopeCreator
    {
        IDisposable CreateReadonly();
    }

    internal class NorthwindScopeCreator : IScopeCreator
    {
        private readonly IScopeContextCreator<NorthwindContext> _scopeContextCreator;

        public NorthwindScopeCreator(IScopeContextCreator<NorthwindContext> scopeContextCreator)
        {
            _scopeContextCreator = scopeContextCreator;
        }

        public IDisposable CreateReadonly() => _scopeContextCreator.CreateReadScope(() => new NorthwindContext());
    }
}
