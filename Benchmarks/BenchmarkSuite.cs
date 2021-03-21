using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace Benchmark
{
    [MemoryDiagnoser]
    public class BenchmarkSuite
    {
        public ToBeSerialized[] ToBeSerialized { get; } = {
            Benchmark.ToBeSerialized.Create(Configuration.Rows, "B"),
            Benchmark.ToBeSerialized.Create(Configuration.Rows)
        };

        private static FlatbuffersBenchmark flatBuffers = new();
        private static NewtonsoftJsonBenchmark newtonsoftJson = new();
        private static MessagePackStringKeyBenchmark messagePackString = new();
        private static MessagePackIntKeyBenchmark messagePackInt = new();
        private static BinaryWriterBenchmark binaryWriter = new();

        public void QuickRun()
        {
            var methods = typeof(BenchmarkSuite).GetMethods();
            foreach (var method in methods) {
                var attrs = method.GetCustomAttributes(true);
                foreach (var attr in attrs) {
                    if (attr.GetType() != typeof(BenchmarkAttribute)) continue;
                    var bytes = (byte[])method.Invoke(this, new object?[] { this.ToBeSerialized[0] });
                }
            }
        }

        public bool Verify()
        {
            var isValid = true;
            var rawData = this.ToBeSerialized[0];

            if (flatBuffers.Verify(rawData) == false) {
                Console.WriteLine("FlatBuffers validation failed.");
                isValid = false;
            }

            if (newtonsoftJson.Verify(rawData) == false) {
                Console.WriteLine("NewtonsoftJson validation failed.");
                isValid = false;
            }

            return isValid;
        }

        public void JustSizes()
        {
            var methodString = new List<string>();
            var paramString = new List<string>();
            var valueString = new List<string>();

            var alternateColors = new[] {
                ConsoleColor.Cyan,
                ConsoleColor.Magenta
            };

            var colors = new List<ConsoleColor> {
                ConsoleColor.White
            };

            var serializedColumn = new SerializedSize();
            var gzipColumn = new GzipSize();
            var gzipB64Column = new GzipBase64Size();

            methodString.Add("Method");
            paramString.Add("Param");
            valueString.Add($"{serializedColumn.ColumnName} | {gzipColumn.ColumnName} | {gzipB64Column.ColumnName}");

            var methods = typeof(BenchmarkSuite).GetMethods();
            for (int i = 0; i < this.ToBeSerialized.Length; i++) {
                var rawData = this.ToBeSerialized[i];
                foreach (var method in methods) {
                    var attrs = method.GetCustomAttributes(true);
                    foreach (var attr in attrs) {
                        if (attr.GetType() != typeof(BenchmarkAttribute)) continue;
                        var bytes = (byte[])method.Invoke(this, new object?[] { rawData });
                        colors.Add(alternateColors[i % alternateColors.Length]);
                        methodString.Add(method.Name);
                        paramString.Add(rawData.ToString());

                        var serializedSize = serializedColumn.GetValue(bytes).PadLeft(serializedColumn.ColumnName.Length, ' ');
                        var gzipSize = gzipColumn.GetValue(bytes).PadLeft(gzipColumn.ColumnName.Length, ' ');
                        var gzipB64Size = gzipB64Column.GetValue(bytes).PadLeft(gzipB64Column.ColumnName.Length, ' ');
                        valueString.Add($"{serializedSize} | {gzipSize} | {gzipB64Size}");
                    }
                }
            }

            var methodPadding = methodString.Max(v => v.Length) + 1;
            var paramPadding = paramString.Max(v => v.Length) + 1;

            for (int i = 0; i < methodString.Count; i++) {
                Console.ForegroundColor = colors[i];
                Console.WriteLine($"| {methodString[i].PadLeft(methodPadding)} | {paramString[i].PadLeft(paramPadding)} | {valueString[i]} |");

                if (i == 0) {
                    Console.WriteLine($"| {new string('-', methodPadding)} | {new string('-', paramPadding)} | {new string('-', serializedColumn.ColumnName.Length)} | {new string('-', gzipColumn.ColumnName.Length)} | {new string('-', gzipB64Column.ColumnName.Length)} |");
                }
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