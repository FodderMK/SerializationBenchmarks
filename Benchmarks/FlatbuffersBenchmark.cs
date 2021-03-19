using FlatBuffers;
using SerializationTests.FlatBuffers;

namespace Benchmark
{
    public class FlatbuffersBenchmark
    {
        private FlatBufferBuilder fbb = new(1024);
        private ToBeSerialized toBeSerialized = ToBeSerialized.Create(Configuration.Rows);

        public byte[] Benchmark()
        {
            var builder = this.fbb;
            var rawData = this.toBeSerialized;

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
            BigData.AddSubData(builder, subDataVector);
            BigData.AddSmallData(builder, smallData);
            builder.Finish(BigData.EndBigData(builder).Value);
            return builder.SizedByteArray();
        }
    }
}