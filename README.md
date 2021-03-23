# Serialization Benchmarks
This is a series of benchmarks showing the speed, size, and garbage of various serialization methods.

## Results
This table is easier to read as raw text.

|            Method |  rawData |       Mean |    Error |   StdDev | Serialized Size | Serialized [gzip] | Serialized [gzip_b64] |    Gen 0 |    Gen 1 |    Gen 2 |  Allocated |
|------------------ |--------- |-----------:|---------:|---------:|----------------:|------------------:|----------------------:|---------:|---------:|---------:|-----------:|
|       FlatBuffers | TBS  [1] | 2,209.4 us |  9.92 us |  8.79 us |       253.98 KB |          80.13 KB |             106.84 KB |  78.1250 |  70.3125 |  70.3125 |  293.09 KB |
|          Protobuf | TBS  [1] | 1,616.3 us |  9.77 us |  9.14 us |       126.71 KB |          42.35 KB |              56.47 KB | 197.2656 |  78.1250 |  39.0625 |  986.35 KB |
|    NewtonsoftJson | TBS  [1] | 6,444.0 us | 64.06 us | 59.92 us |       398.32 KB |          59.24 KB |              78.98 KB | 593.7500 | 515.6250 | 328.1250 | 2779.38 KB |
| MessagePackString | TBS  [1] | 1,104.3 us | 10.34 us |  8.63 us |       409.49 KB |          56.37 KB |              75.16 KB | 123.0469 | 123.0469 | 123.0469 |  819.77 KB |
|    MessagePackInt | TBS  [1] |   778.4 us |  6.64 us |  6.21 us |        96.93 KB |          49.94 KB |              66.59 KB |  92.7734 |  60.5469 |  30.2734 |  507.21 KB |
|      BinaryWriter | TBS  [1] |   647.1 us |  3.85 us |  3.41 us |        78.14 KB |          42.76 KB |              57.02 KB |  82.0313 |  41.0156 |  41.0156 |  334.64 KB |
|       FlatBuffers | TBS [32] | 2,547.4 us | 19.43 us | 16.22 us |       566.52 KB |         329.66 KB |             439.54 KB | 144.5313 | 136.7188 | 136.7188 |  605.62 KB |
|          Protobuf | TBS [32] | 1,664.9 us | 16.57 us | 15.50 us |       429.48 KB |         272.67 KB |             363.56 KB | 265.6250 | 212.8906 | 109.3750 | 1289.92 KB |
|    NewtonsoftJson | TBS [32] | 7,211.8 us | 45.65 us | 42.70 us |       701.08 KB |         282.26 KB |             376.34 KB | 867.1875 | 789.0625 | 492.1875 | 4299.71 KB |
| MessagePackString | TBS [32] | 1,149.0 us | 22.33 us | 28.24 us |       722.02 KB |         289.90 KB |             386.53 KB | 259.7656 | 207.0313 | 166.0156 | 1132.88 KB |
|    MessagePackInt | TBS [32] | 1,004.6 us |  5.71 us |  5.06 us |       409.46 KB |         274.56 KB |             366.08 KB | 123.0469 | 123.0469 | 123.0469 |  819.76 KB |
|      BinaryWriter | TBS [32] |   974.1 us | 18.57 us | 27.23 us |       380.91 KB |         266.44 KB |             355.26 KB | 341.7969 | 310.5469 | 309.5703 | 1406.09 KB |

## Notes
- For `TBS [##]`, ## represents the length of the strings used.
- The Flatbuffers folder is directly copied from the official git repository.
- flatc is copied from the releases section of the official git repository.
- protoc is copied from the releases section of the official git repository