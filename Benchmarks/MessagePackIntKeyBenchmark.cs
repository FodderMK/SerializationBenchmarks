using MessagePack;

namespace Benchmark
{
    public class MessagePackIntKeyBenchmark
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

        [MessagePackObject]
        public class BigData
        {
            [Key(0)] public int IntValue;
            [Key(1)] public bool BoolValue;
            [Key(2)] public string StringValue;
            [Key(3)] public double DoubleValue;
            [Key(4)] public SubData[] SubData;
            [Key(5)] public SmallData[] SmallData;
        }

        [MessagePackObject]
        public class SubData
        {
            [Key(0)] public string StringValue;
            [Key(1)] public int IntValue;
        }

        [MessagePackObject]
        public struct SmallData
        {
            [Key(0)] public short ShortValue;
        }
    }
}