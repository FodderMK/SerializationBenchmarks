using BenchmarkDotNet.Running;

namespace Benchmark
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            if (new BenchmarkSuite().Verify() == false) {
                return;
            }

            BenchmarkRunner.Run<BenchmarkSuite>(Configuration.BenchmarkConfig);
            // new BenchmarkSuite().JustSizes();
        }
    }
}