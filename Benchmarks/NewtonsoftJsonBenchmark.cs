using System.Text;
using Newtonsoft.Json;

namespace Benchmark
{
    public class NewtonsoftJsonBenchmark
    {
        private ToBeSerialized toBeSerialized = ToBeSerialized.Create(Configuration.Rows);

        public byte[] Benchmark()
        {
            var json = JsonConvert.SerializeObject(this.toBeSerialized, Formatting.None);
            return Encoding.UTF8.GetBytes(json);
        }
    }
}