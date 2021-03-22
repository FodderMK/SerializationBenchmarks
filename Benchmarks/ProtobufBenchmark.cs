using Google.Protobuf;
using SerializationTests.Protobuf;

namespace Benchmark
{
    public class ProtobufBenchmark
    {
        public byte[] Benchmark(ToBeSerialized rawData)
        {
            var bigData = new BigData {
                IntValue = rawData.IntValue,
                BoolValue = rawData.BoolValue,
                StringValue = rawData.StringValue,
                DoubleValue = rawData.DoubleValue,
            };

            bigData.SubData.Capacity = rawData.SubData.Length;
            bigData.SmallData.Capacity = rawData.SmallData.Length;

            for (int i = 0; i < rawData.SubData.Length; i++) {
                var row = rawData.SubData[i];
                bigData.SubData.Add(new SubData {
                    StringValue = row.StringValue,
                    IntValue = row.IntValue
                });
            }

            for (int i = 0; i < rawData.SmallData.Length; i++) {
                bigData.SmallData.Add(new SmallData {
                    ShortValue = rawData.SmallData[i]
                });
            }

            return bigData.ToByteArray();
        }

        public bool Verify(ToBeSerialized rawData)
        {
            var serialized = this.Benchmark(rawData);
            var uncompressed = Utilities.GzipCompressAndDecompress(serialized);
            var unserialized = BigData.Parser.ParseFrom(uncompressed);

            var isValid = unserialized.StringValue == rawData.StringValue;

            for (int i = 0; i < rawData.SubData.Length; i++) {
                isValid = isValid && rawData.SubData[i].StringValue == unserialized.SubData[i].StringValue && rawData.SubData[i].IntValue == unserialized.SubData[i].IntValue;
            }

            return isValid;
        }
    }
}