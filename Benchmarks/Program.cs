using BenchmarkDotNet.Running;

namespace Benchmark
{
    internal static class Program
    {
        private static void Main()
        {
            if (new BenchmarkSuite().Verify() == false) {
                return;
            }

            BenchmarkRunner.Run<BenchmarkSuite>(Configuration.BenchmarkConfig);
            // new BenchmarkSuite().JustSizes();
        }
    }
}