using System;
using System.Linq;
using System.Security.Cryptography;

namespace Benchmark
{
    [Serializable]
    public class ToBeSerialized
    {
        public int IntValue { get; set; }
        public bool BoolValue { get; set; }
        public string StringValue { get; set; }
        public double DoubleValue { get; set; }
        public SubDataToBeSerialized[] SubData { get; set; }
        public short[] SmallData { get; set; }

        public static ToBeSerialized Create(int rows, int stringLength)
        {
            var output = new ToBeSerialized {
                IntValue = 1,
                BoolValue = true,
                StringValue = RandomString(stringLength),
                DoubleValue = 1e120,
                SubData = new SubDataToBeSerialized[rows],
                SmallData = new short[rows]
            };

            for (var i = 0; i < rows; i++) {
                output.SubData[i] = new SubDataToBeSerialized {
                    StringValue = RandomString(stringLength),
                    IntValue = i
                };

                output.SmallData[i] = (short)(i % short.MaxValue);
            }

            return output;
        }

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[RandomNumberGenerator.GetInt32(s.Length)]).ToArray());
        }

        public override string ToString()
        {
            return "TBS " + ("[" + this.StringValue.Length + "]").PadLeft(4, ' ');
        }
    }

    [Serializable]
    public struct SubDataToBeSerialized
    {
        public string StringValue { get; set; }
        public int IntValue { get; set; }
    }
}