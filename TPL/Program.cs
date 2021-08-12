using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TPL
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var samples = new List<IAsyncCodeSample>
            {
                new ParallelExtensions(),
                /*new NonAsyncSample(),
                new Syncronization(),
                new Cancellation(),
                new DuckTypingAwaitable(),*/
            };
            foreach (var asyncCodeSample in samples)
            {
                Console.WriteLine(asyncCodeSample.SampleDescription);
                await asyncCodeSample.Run();
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }
        }
    }
}
