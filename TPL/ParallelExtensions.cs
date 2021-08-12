using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace TPL
{
    public class ParallelExtensions : IAsyncCodeSample
    {
        /// <inheritdoc cref="IAsyncCodeSample"/>
        public Task Run()
        {
            var row1 = new[] { 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2};
            var row2 = new[] { 0, 5, 0, 0, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2};
            var row3 = new[] { 2, 0, 3, 3, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2};
            var row4 = new[] { 2, 0, 3, 3, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2};
            var row5 = new[] { 2, 0, 3, 3, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2};
            var row6 = new[] { 2, 0, 3, 3, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2};
            var row7 = new[] { 2, 0, 3, 3, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2};
            var row8 = new[] { 2, 0, 3, 3, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2};
            var row9 = new[] { 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2};
            var row10 = new[] { 0, 5, 0, 0, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2};
            var row11 = new[] { 2, 0, 3, 3, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2};
            var row12 = new[] { 2, 0, 3, 3, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2};
            var row13 = new[] { 2, 0, 3, 3, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2};
            var row14 = new[] { 2, 0, 3, 3, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2};
            var row15 = new[] { 2, 0, 3, 3, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2};
            var row16 = new[] { 2, 0, 3, 3, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2};
            var lists = new[]
            {
                row1,
                row2,
                row3,
                row4,
                row5,
                row6,
                row7,
                row8,
                row9,
                row10,
                row11,
                row12,
                row13,
                row14,
                row15,
                row16,
            };
            var timerSync = new Stopwatch();
            timerSync.Start();
            var sumSync = (from row in lists select row.Sum()).Sum();
            timerSync.Stop();
            Console.WriteLine($"Sum of elements in matrix for linq sync method: {sumSync} with time {timerSync.ElapsedMilliseconds}ms");


            var timerForeach = new Stopwatch();
            timerForeach.Start();
            int sumForeach = 0;
            foreach (var list in lists)
            {
                foreach (var element in list)
                {
                    sumForeach += element;
                }
            }

            timerForeach.Stop();
            Console.WriteLine($"Sum of elements in matrix for foreach sync method: {sumForeach} with time {timerForeach.ElapsedMilliseconds}ms");

            var timerAsync = new Stopwatch();
            timerAsync.Start();
            var sumAsync = (from row in lists.AsParallel() select row.Sum()).Sum();
            timerAsync.Stop();
            Console.WriteLine($"Sum of elements in matrix for async method: {sumAsync} with time {timerAsync.ElapsedMilliseconds}ms");
            return Task.CompletedTask;
        }

        /// <inheritdoc cref="IAsyncCodeSample"/>
        public string SampleDescription => "Demonstrates usage of parallel extensions.";
    }
}
