using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Benchmark
{
    [MemoryDiagnoser]
    public class BenchmarkSuite
    {
        public ToBeSerialized[] ToBeSerialized { get; } = {
            Benchmark.ToBeSerialized.Create(Configuration.Rows),
            Benchmark.ToBeSerialized.Create(Configuration.Rows, "B")
        };

        private static FlatbuffersBenchmark flatBuffers = new();
        private static NewtonsoftJsonBenchmark newtonsoftJson = new();
        private static MessagePackStringKeyBenchmark messagePackString = new();
        private static MessagePackIntKeyBenchmark messagePackInt = new();
        private static BinaryWriterBenchmark binaryWriter = new();

        public void QuickRun()
        {
            flatBuffers.Benchmark(this.ToBeSerialized[0]);
            newtonsoftJson.Benchmark(this.ToBeSerialized[0]);
            messagePackString.Benchmark(this.ToBeSerialized[0]);
            messagePackInt.Benchmark(this.ToBeSerialized[0]);
            binaryWriter.Benchmark(this.ToBeSerialized[0]);
        }
        }

        [Benchmark]
        [ArgumentsSource(nameof(ToBeSerialized))]
        public byte[] FlatBuffers(ToBeSerialized rawData)
        {
            return flatBuffers.Benchmark(rawData);
        }

        [Benchmark]
        [ArgumentsSource(nameof(ToBeSerialized))]
        public byte[] NewtonsoftJson(ToBeSerialized rawData)
        {
            return newtonsoftJson.Benchmark(rawData);
        }

        [Benchmark]
        [ArgumentsSource(nameof(ToBeSerialized))]
        public byte[] MessagePackString(ToBeSerialized rawData)
        {
            return messagePackString.Benchmark(rawData);
        }

        [Benchmark]
        [ArgumentsSource(nameof(ToBeSerialized))]
        public byte[] MessagePackInt(ToBeSerialized rawData)
        {
            return messagePackInt.Benchmark(rawData);
        }

        [Benchmark]
        [ArgumentsSource(nameof(ToBeSerialized))]
        public byte[] BinaryWriter(ToBeSerialized rawData)
        {
            return binaryWriter.Benchmark(rawData);
        }
    }
}