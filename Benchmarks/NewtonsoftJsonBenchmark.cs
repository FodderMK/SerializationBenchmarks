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
    }
}