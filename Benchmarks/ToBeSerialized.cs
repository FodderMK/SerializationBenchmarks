using System;

namespace Benchmark
{
    [Serializable]
    public class ToBeSerialized
    {
        public int IntValue { get; set; }
        public bool BoolValue { get; set; }
        public string StringValue { get; set; }
        public SubDataToBeSerialized[] SubData { get; set; }
        public short[] SmallData { get; set; }

        public static ToBeSerialized Create(int rows)
        {
            var output = new ToBeSerialized {
                IntValue = 1,
                BoolValue = true,
                StringValue = Configuration.DefaultString,
                SubData = new SubDataToBeSerialized[rows],
                SmallData = new short[rows]
            };

            for (var i = 0; i < rows; i++) {
                output.SubData[i] = new SubDataToBeSerialized {
                    StringValue = Configuration.DefaultString,
                    IntValue = i
                };

                output.SmallData[i] = (short)(i % short.MaxValue);
            }

            return output;
        }
    }

    [Serializable]
    public struct SubDataToBeSerialized
    {
        public string StringValue { get; set; }
        public int IntValue { get; set; }
    }
}