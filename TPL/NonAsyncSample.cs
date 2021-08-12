using System;
using System.Threading.Tasks;

namespace TPL
{
    public class NonAsyncSample : IAsyncCodeSample
    {
        /// <inheritdoc cref="IAsyncCodeSample"/>
        public Task Run()
        {
            Console.WriteLine("Doing nothing asynchronous...");
            return Task.CompletedTask;
        }

        /// <inheritdoc cref="IAsyncCodeSample"/>
        public string SampleDescription => "This shows how to satisfy TPL contracts for synchronous code.";
    }
}
