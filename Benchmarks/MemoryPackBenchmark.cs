using MemoryPack;

namespace Benchmark
{
    public class MemoryPackBenchmark
    {
        public byte[] Benchmark(ToBeSerialized rawData)
        {
            var bigData = new MemoryPackBigData {
                IntValue = rawData.IntValue,
                BoolValue = rawData.BoolValue,
                StringValue = rawData.StringValue,
                DoubleValue = rawData.DoubleValue,
                SubData = new MemoryPackSubData[rawData.SubData.Length],
                SmallData = new MemoryPackSmallData[rawData.SmallData.Length]
            };

            for (int i = 0; i < rawData.SubData.Length; i++) {
                var row = rawData.SubData[i];
                bigData.SubData[i] = new MemoryPackSubData {
                    IntValue = row.IntValue,
                    StringValue = row.StringValue
                };
            }

            for (int i = 0; i < rawData.SmallData.Length; i++) {
                bigData.SmallData[i].ShortValue = rawData.SmallData[i];
            }

            return MemoryPackSerializer.Serialize(bigData);
        }

        public bool Verify(ToBeSerialized rawData)
        {
            var serialized = this.Benchmark(rawData);
            var serializedCompressed = Utilities.GzipCompress(serialized);
            var serializedUncompressed = Utilities.GzipDecompress(serializedCompressed);
            var unserialized = MemoryPackSerializer.Deserialize<MemoryPackBigData>(serializedUncompressed);
            return unserialized.StringValue == rawData.StringValue;
        }
    }

    [MemoryPackable]
    public partial class MemoryPackBigData
    {
        public int IntValue;
        public bool BoolValue;
        public string StringValue;
        public double DoubleValue;
        public MemoryPackSubData[] SubData;
        public MemoryPackSmallData[] SmallData;
    }

    [MemoryPackable]
    public partial class MemoryPackSubData
    {
        public string StringValue;
        public int IntValue;
    }

    [MemoryPackable]
    public partial struct MemoryPackSmallData
    {
        public short ShortValue;
    }
}