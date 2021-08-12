using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace TPL
{
    public class DuckTypingAwaitable : IAsyncCodeSample
    {
        /// <inheritdoc cref="IAsyncCodeSample"/>
        public async Task Run()
        {
            Console.WriteLine("Starting breaking .NET.....");
            var context = new Context();
            await context;
            Console.WriteLine(".NET survived!");
        }

        /// <inheritdoc cref="IAsyncCodeSample"/>
        public string SampleDescription =>
            "This is how you can implement your custom awaitable and break .NET in a process.";

        public struct Context : INotifyCompletion
        {
            public void OnCompleted(Action continuation)
            {
            }

            public bool IsCompleted => false;

            public Context GetAwaiter() => this;

            public void GetResult() { }
        }
    }
}
