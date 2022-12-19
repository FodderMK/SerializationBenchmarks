using System.IO;

namespace Benchmark
{
    public class BinaryWriterBenchmark
    {
        public byte[] Benchmark(ToBeSerialized rawData)
        {
            using var memoryStream = new MemoryStream();
            using var writer = new BinaryWriter(memoryStream);

            var subDataLength = rawData.SubData.Length;
            var smallDataLength = rawData.SmallData.Length;

            writer.Write(rawData.IntValue);
            writer.Write(rawData.BoolValue);
            writer.Write(rawData.StringValue);
            writer.Write(rawData.DoubleValue);

            writer.Write((short)subDataLength);
            for (int i = 0; i < subDataLength; i++) {
                var row = rawData.SubData[i];
                writer.Write(row.IntValue);
                writer.Write(row.StringValue);
            }

            writer.Write((short)smallDataLength);
            for (int i = 0; i < smallDataLength; i++) {
                writer.Write(rawData.SmallData[i]);
            }

            return memoryStream.ToArray();
        }
    }
}