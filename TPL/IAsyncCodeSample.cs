using System;
using System.Threading.Tasks;

namespace TPL
{
    public interface IAsyncCodeSample
    {
        /// <summary>
        /// Runs async code example
        /// </summary>
        /// <returns>Task to be executed.</returns>
        public Task Run();

        /// <summary>
        /// Description of the example.
        /// </summary>
        public string SampleDescription { get; }
        
        /// <summary>
        /// Display the description of the case. English only.
        /// </summary>
        public void DisplayExampleDescription() => Console.WriteLine(SampleDescription);
    }
}
