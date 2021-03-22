using System;
using System.IO;
using System.IO.Compression;
using BenchmarkDotNet.Running;

namespace Benchmark
{
    public static class Utilities
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

        public static byte[] GzipCompressAndDecompress(byte[] bytes)
        {
            return GzipDecompress(GzipCompress(bytes));
        }

        public static byte[] GzipCompress(byte[] bytes)
        {
            using var inputStream = new MemoryStream(bytes);
            using var outputStream = new MemoryStream();
            using var gzipStream = new GZipStream(outputStream, CompressionMode.Compress);
            inputStream.CopyTo(gzipStream);
            gzipStream.Flush();
            return outputStream.ToArray();
        }

        public static byte[] GzipDecompress(byte[] bytes)
        {
            using var inputStream = new MemoryStream(bytes);
            using var gzipStream = new GZipStream(inputStream, CompressionMode.Decompress);
            using var outputStream = new MemoryStream();
            gzipStream.CopyTo(outputStream);
            return outputStream.ToArray();
        }
    }
}