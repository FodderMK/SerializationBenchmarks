using System.Text;
using Newtonsoft.Json;

namespace Benchmark
{
    public class NewtonsoftJsonBenchmark
    {
        public byte[] Benchmark(ToBeSerialized rawData)
        {
            var json = JsonConvert.SerializeObject(rawData, Formatting.None);
            return Encoding.UTF8.GetBytes(json);
        }

        public bool Verify(ToBeSerialized rawData)
        {
            var serialized = this.Benchmark(rawData);
            var compressed = Utilities.GzipCompress(serialized);
            var uncompressed = Encoding.UTF8.GetString(Utilities.GzipDecompress(compressed));
            var unserialized = JsonConvert.DeserializeObject<ToBeSerialized>(uncompressed);
            return unserialized != null && unserialized.StringValue == rawData.StringValue;
        }
    }
}