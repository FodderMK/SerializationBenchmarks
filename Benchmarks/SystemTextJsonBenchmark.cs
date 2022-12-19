using System.Text;
using System.Text.Json;

namespace Benchmark
{
    public class SystemTextJsonBenchmark
    {
        private JsonSerializerOptions options = new() {
            WriteIndented = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public byte[] Benchmark(ToBeSerialized rawData)
        {
            var json = JsonSerializer.Serialize(rawData, this.options);
            return Encoding.UTF8.GetBytes(json);
        }

        public bool Verify(ToBeSerialized rawData)
        {
            var serialized = this.Benchmark(rawData);
            var compressed = Utilities.GzipCompress(serialized);
            var uncompressed = Encoding.UTF8.GetString(Utilities.GzipDecompress(compressed));
            var unserialized = JsonSerializer.Deserialize<ToBeSerialized>(uncompressed, this.options);
            return unserialized != null && unserialized.StringValue == rawData.StringValue;
        }
    }
}