using System;
using System.Threading;
using System.Threading.Tasks;

namespace TPL
{
    public class Cancellation : IAsyncCodeSample
    {
        /// <inheritdoc cref="IAsyncCodeSample"/>
        public async Task Run()
        {
            try
            {
                // 2500 sets timeout in ms after which Cancel() method will be called.
                using var tokenSource = new CancellationTokenSource(2500);
                var token = tokenSource.Token;
                await DoAsyncWork(token);
                await Task.Delay(5000, token);
                tokenSource.Cancel(); // manual cancel
                CancelableWork(CancellationToken.None); // use this options if you do not want to encounter cancellation
                CancelableWork(token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Catching cancellation exception...");
            }
        }

        /// <summary>
        /// Use token throw at the beginning of the method that should be interrupted.
        /// </summary>
        /// <param name="token"></param>
        private static void CancelableWork(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            Console.WriteLine("Big work completed without cancellation!");
        }

        private static Task DoAsyncWork(CancellationToken token = default) => Task.Delay(500, token);

        /// <inheritdoc cref="IAsyncCodeSample"/>
        public string SampleDescription => "This is the correct way to cancel long running async jobs." +
                                           "Propagate cancellation token in all your async methods except cases where cancellation might harm your business logic.";
    }
}
