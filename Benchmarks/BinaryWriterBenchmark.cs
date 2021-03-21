using System.IO;

namespace Benchmark
{
    public class BinaryWriterBenchmark
    {
        public byte[] Benchmark(ToBeSerialized rawData)
        {
            using var memoryStream = new MemoryStream();
            using var writer = new BinaryWriter(memoryStream);

            writer.Write(rawData.IntValue);
            writer.Write(rawData.BoolValue);
            writer.Write(rawData.StringValue);
            writer.Write(rawData.DoubleValue);
            writer.Write((short)rawData.SubData.Length);
            for (int i = 0; i < rawData.SubData.Length; i++) {
                var row = rawData.SubData[i];
                writer.Write(row.IntValue);
                writer.Write(row.StringValue);
            }

            writer.Write((short)rawData.SmallData.Length);
            for (int i = 0; i < rawData.SmallData.Length; i++) {
                writer.Write(rawData.SmallData[i]);
            }

            return memoryStream.ToArray();
        }
    }
}