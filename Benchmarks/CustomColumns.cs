using System;
using System.IO;
using System.IO.Compression;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace Benchmark
{
    public static class ColumnHelper
    {
        private static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        public static string SizeSuffix(long value, int decimalPlaces = 1)
        {
            if (value < 0) {
                return "-" + SizeSuffix(-value, decimalPlaces);
            }

            int i = 0;
            decimal dValue = value;
            while (Math.Round(dValue, decimalPlaces) >= 1000) {
                dValue /= 1024;
                i++;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}", dValue, SizeSuffixes[i]);
        }

        public static bool TryGetBytes(BenchmarkCase benchmarkCase, out byte[] bytes)
        {
            var method = benchmarkCase.Descriptor.WorkloadMethod;
            if (method.ReturnType == typeof(byte[])) {
                var obj = Activator.CreateInstance(benchmarkCase.Descriptor.Type, null);
                bytes = (byte[])method.Invoke(obj, new[] { benchmarkCase.Parameters[0].Value });
                return true;
            }

            bytes = Array.Empty<byte>();
            return false;
        }
    }

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
            if (ColumnHelper.TryGetBytes(benchmarkCase, out byte[] bytes) == false) {
                return "--";
            }

            return this.GetValue(bytes);
        }

        public string GetValue(byte[] bytes)
        {
            return $"{ColumnHelper.SizeSuffix(bytes.Length, 2)}";
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
            if (ColumnHelper.TryGetBytes(benchmarkCase, out byte[] bytes) == false) {
                return "--";
            }

            return this.GetValue(bytes);
        }

        public string GetValue(byte[] bytes)
        {
            using var compressedStream = new MemoryStream();
            using var zipStream = new GZipStream(compressedStream, CompressionMode.Compress);
            zipStream.Write(bytes, 0, bytes.Length);
            var compressed = compressedStream.ToArray();
            return ColumnHelper.SizeSuffix(compressed.Length, 2);
        }
    }

    public class GzipBase64Size : IColumn
    {
        public string Id => nameof(GzipBase64Size);
        public string ColumnName => "Serialized [gzip-b64]";
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
            if (ColumnHelper.TryGetBytes(benchmarkCase, out byte[] bytes) == false) {
                return "--";
            }

            return this.GetValue(bytes);
        }

        public string GetValue(byte[] bytes)
        {
            using var compressedStream = new MemoryStream();
            using var zipStream = new GZipStream(compressedStream, CompressionMode.Compress);
            zipStream.Write(bytes, 0, bytes.Length);
            var compressed = compressedStream.ToArray();
            var encoded = Convert.ToBase64String(compressed);
            return ColumnHelper.SizeSuffix(encoded.Length, 2);
        }
    }
}