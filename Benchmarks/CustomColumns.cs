using System;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace Benchmark
{
    public class SerializedSize : IColumn
    {
        public string Id => nameof(SerializedSize);
        public string ColumnName => "Serialized Size";
        public bool AlwaysShow => true;
        public ColumnCategory Category => ColumnCategory.Baseline;
        public int PriorityInCategory => 10;
        public bool IsNumeric => true;
        public UnitType UnitType => UnitType.Size;
        public string Legend => "Size of the serialized object";

        public string GetValue(Summary summary, BenchmarkCase benchmarkCase) => GetValue(summary, benchmarkCase, SummaryStyle.Default);
        public bool IsDefault(Summary summary, BenchmarkCase benchmarkCase) => false;
        public bool IsAvailable(Summary summary) => true;

        public string GetValue(Summary summary, BenchmarkCase benchmarkCase, SummaryStyle style)
        {
            if (Utilities.TryGetBytes(benchmarkCase, out byte[] bytes) == false) {
                return "--";
            }

            return this.GetValue(bytes);
        }

        public string GetValue(byte[] bytes)
        {
            return $"{Utilities.SizeSuffix(bytes.Length, 2)}";
        }
    }

    public class GzipSize : IColumn
    {
        public string Id => nameof(GzipSize);
        public string ColumnName => "Serialized [gzip]";
        public bool AlwaysShow => true;
        public ColumnCategory Category => ColumnCategory.Baseline;
        public int PriorityInCategory => 11;
        public bool IsNumeric => true;
        public UnitType UnitType => UnitType.Size;
        public string Legend => "Size of the serialized object compressed with gzip";

        public string GetValue(Summary summary, BenchmarkCase benchmarkCase) => GetValue(summary, benchmarkCase, SummaryStyle.Default);
        public bool IsDefault(Summary summary, BenchmarkCase benchmarkCase) => false;
        public bool IsAvailable(Summary summary) => true;

        public string GetValue(Summary summary, BenchmarkCase benchmarkCase, SummaryStyle style)
        {
            if (Utilities.TryGetBytes(benchmarkCase, out byte[] bytes) == false) {
                return "--";
            }

            return this.GetValue(bytes);
        }

        public string GetValue(byte[] bytes)
        {
            var compressed = Utilities.GzipCompress(bytes);
            return Utilities.SizeSuffix(compressed.Length, 2);
        }
    }

    public class GzipBase64Size : IColumn
    {
        public string Id => nameof(GzipBase64Size);
        public string ColumnName => "Serialized [gzip_b64]";
        public bool AlwaysShow => true;
        public ColumnCategory Category => ColumnCategory.Baseline;
        public int PriorityInCategory => 12;
        public bool IsNumeric => true;
        public UnitType UnitType => UnitType.Size;
        public string Legend => "Size of the serialized object compressed with gzip and base64-encoded(bytes)";

        public string GetValue(Summary summary, BenchmarkCase benchmarkCase) => GetValue(summary, benchmarkCase, SummaryStyle.Default);
        public bool IsDefault(Summary summary, BenchmarkCase benchmarkCase) => false;
        public bool IsAvailable(Summary summary) => true;

        public string GetValue(Summary summary, BenchmarkCase benchmarkCase, SummaryStyle style)
        {
            if (Utilities.TryGetBytes(benchmarkCase, out byte[] bytes) == false) {
                return "--";
            }

            return this.GetValue(bytes);
        }

        public string GetValue(byte[] bytes)
        {
            var compressed = Utilities.GzipCompress(bytes);
            var encoded = Convert.ToBase64String(compressed);
            return Utilities.SizeSuffix(encoded.Length, 2);
        }
    }
}