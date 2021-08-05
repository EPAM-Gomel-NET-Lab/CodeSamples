using System;
using BenchmarkDotNet.Running;

namespace PerformanceDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            BenchmarkRunner.Run<GenericsBenchmark>();
            Console.ReadLine();
        }
    }
}
