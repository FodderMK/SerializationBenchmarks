# Serialization Benchmarks
This is a series of benchmarks showing the speed, size, and garbage of various serialization methods.

## Results

| Method            |  rawData |      Mean |   Error |   StdDev |  Serialized Size | Serialized [gzip] |  Serialized [gzip_b64] |    Gen 0 |    Gen 1 |    Gen 2 |  Allocated |
|-------------------|----------|----------:|--------:|---------:|-----------------:|------------------:|-----------------------:|---------:|---------:|---------:|-----------:|
| FlatBuffers       | TBS  [1] | 1,338.4us | 12.67us |  11.85us |         253.98KB |           80.13KB |               106.84KB |  72.2656 |  70.3125 |  70.3125 |   293.12KB |
| Protobuf          | TBS  [1] | 1,004.9us | 15.34us |  14.35us |         126.71KB |           42.33KB |                56.43KB |  78.1250 |  39.0625 |  39.0625 |   986.37KB |
| NewtonsoftJson    | TBS  [1] | 4,072.8us | 46.98us |  43.95us |         398.32KB |           59.15KB |                78.87KB | 421.8750 | 367.1875 | 328.1250 |  2779.27KB |
| SystemTextJson    | TBS  [1] | 1,565.6us | 15.80us |  14.01us |         398.32KB |           59.21KB |                78.95KB | 347.6563 | 330.0781 | 330.0781 |  1509.09KB |
| MessagePackString | TBS  [1] |   561.1us |  8.64us |   8.08us |         409.49KB |           56.39KB |                75.19KB | 124.0234 | 124.0234 | 124.0234 |   819.82KB |
| MessagePackInt    | TBS  [1] |   438.5us |  5.62us |   5.26us |          96.93KB |           49.95KB |                66.60KB |  30.7617 |  30.7617 |  30.7617 |   507.22KB |
| MemoryPack        | TBS  [1] |   357.0us |  6.62us |   6.19us |         156.28KB |           45.34KB |                60.46KB |  49.8047 |  49.8047 |  49.8047 |   566.58KB |
| BinaryWriter      | TBS  [1] |   313.8us |  4.21us |   3.94us |          78.14KB |           42.76KB |                57.01KB |  41.5039 |  41.5039 |  41.5039 |   334.27KB |
| FlatBuffers       | TBS [32] | 1,397.1us |  9.66us |   9.03us |         566.52KB |          329.58KB |               439.44KB | 140.6250 | 138.6719 | 138.6719 |   605.67KB |
| Protobuf          | TBS [32] | 1,103.2us | 15.75us |  14.73us |         429.48KB |          272.60KB |               363.46KB | 158.2031 | 130.8594 | 107.4219 |  1289.98KB |
| NewtonsoftJson    | TBS [32] | 4,345.5us | 69.14us |  64.67us |         701.08KB |          282.39KB |               376.52KB | 625.0000 | 570.3125 | 492.1875 |  4299.79KB |
| SystemTextJson    | TBS [32] | 1,825.2us | 32.74us |  32.15us |         701.08KB |          282.21KB |               376.28KB | 435.5469 | 417.9688 | 417.9688 |  2417.04KB |
| MessagePackString | TBS [32] |   684.6us | 13.29us |  17.28us |         722.02KB |          289.92KB |               386.57KB | 189.4531 | 175.7813 | 165.0391 |  1132.82KB |
| MessagePackInt    | TBS [32] |   534.6us |  7.63us |   7.14us |         409.46KB |          274.55KB |               366.07KB | 124.0234 | 124.0234 | 124.0234 |   819.79KB |
| MemoryPack        | TBS [32] |   437.3us |  6.05us |   5.66us |         459.04KB |          268.76KB |               358.35KB | 138.1836 | 125.4883 | 113.2813 |   869.85KB |
| BinaryWriter      | TBS [32] |   438.0us |  8.64us |  10.62us |         380.91KB |          266.48KB |               355.30KB | 316.4063 | 309.0820 | 308.5938 |  1405.16KB |

## Notes
- For `TBS [##]`, ## represents the length of the strings used.
- The exact Mean/Error/StdDev numbers aren't important, the relative differences are 
- flatc is copied from the [releases](https://github.com/google/flatbuffers/releases/tag/v22.12.06) section of the official git repository.
- protoc is copied from the [releases](https://github.com/protocolbuffers/protobuf/releases/tag/v21.12) section of the official git repository