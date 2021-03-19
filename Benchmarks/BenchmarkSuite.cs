using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;

namespace Benchmark
{
    [MemoryDiagnoser]
    public class BenchmarkSuite
    {
        private static FlatbuffersBenchmark flatBuffers = new();
        private static NewtonsoftJsonBenchmark newtonsoftJson = new();

        public void QuickRun()
        {
            flatBuffers.Benchmark();
            newtonsoftJson.Benchmark();
        }

        [Benchmark]
        public byte[] FlatBuffers()
        {
            return flatBuffers.Benchmark();
        }

        [Benchmark]
        public byte[] NewtonsoftJson()
        {
            return newtonsoftJson.Benchmark();
        }

    }
}