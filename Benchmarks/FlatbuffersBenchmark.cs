using FlatBuffers;
using SerializationTests.FlatBuffers;

namespace Benchmark
{
    public class FlatbuffersBenchmark
    {
        private FlatBufferBuilder fbb = new(1024);
        public byte[] Benchmark(ToBeSerialized rawData)
        {
            var builder = this.fbb;

            builder.Clear();

            var subdataOffset = new Offset<SubData>[rawData.SubData.Length];
            for (int i = 0; i < rawData.SubData.Length; i++) {
                var row = rawData.SubData[i];
                subdataOffset[i] = SubData.CreateSubData(builder, builder.CreateString(row.StringValue), row.IntValue);
            }


            BigData.StartSmallDataVector(builder, rawData.SmallData.Length);
            for (int i = 0; i < rawData.SmallData.Length; i++) {
                SmallData.CreateSmallData(builder, rawData.SmallData[i]);
            }

            var smallData = builder.EndVector();

            var subDataVector = BigData.CreateSubDataVector(builder, subdataOffset);
            var stringValue = builder.CreateString(rawData.StringValue);

            BigData.StartBigData(builder);
            BigData.AddIntValue(builder, rawData.IntValue);
            BigData.AddBoolValue(builder, rawData.BoolValue);
            BigData.AddStringValue(builder, stringValue);
            BigData.AddDoubleValue(builder, rawData.DoubleValue);
            BigData.AddSubData(builder, subDataVector);
            BigData.AddSmallData(builder, smallData);
            builder.Finish(BigData.EndBigData(builder).Value);
            return builder.SizedByteArray();
        }
    }
}