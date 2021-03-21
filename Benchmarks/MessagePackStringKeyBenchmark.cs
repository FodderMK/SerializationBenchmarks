using MessagePack;

namespace Benchmark
{
    public class MessagePackStringKeyBenchmark
    {
        public byte[] Benchmark(ToBeSerialized rawData)
        {
            var bigData = new BigData() {
                IntValue = rawData.IntValue,
                BoolValue = rawData.BoolValue,
                StringValue = rawData.StringValue,
                DoubleValue = rawData.DoubleValue,
                SubData = new SubData[rawData.SubData.Length],
                SmallData = new SmallData[rawData.SmallData.Length]
            };

            for (int i = 0; i < rawData.SubData.Length; i++) {
                var row = rawData.SubData[i];
                bigData.SubData[i] = new SubData {
                    IntValue = row.IntValue,
                    StringValue = row.StringValue
                };
            }

            for (int i = 0; i < rawData.SmallData.Length; i++) {
                bigData.SmallData[i].ShortValue = rawData.SmallData[i];
            }

            return MessagePackSerializer.Serialize(bigData);
        }

        [MessagePackObject(keyAsPropertyName: true)]
        public class BigData
        {
            public int IntValue;
            public bool BoolValue;
            public string StringValue;
            public double DoubleValue;
            public SubData[] SubData;
            public SmallData[] SmallData;
        }

        [MessagePackObject(keyAsPropertyName: true)]
        public class SubData
        {
            public string StringValue;
            public int IntValue;
        }

        [MessagePackObject(keyAsPropertyName: true)]
        public struct SmallData
        {
            public short ShortValue;
        }
    }
}