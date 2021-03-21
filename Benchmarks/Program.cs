using BenchmarkDotNet.Running;

namespace Benchmark
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            new BenchmarkSuite().QuickRun();
            // BenchmarkRunner.Run<BenchmarkSuite>(Configuration.BenchmarkConfig);
            new BenchmarkSuite().JustSizes();
        }
    }
}