# Serialization Benchmarks
This is a series of benchmarks showing the speed, size, and garbage of various serialization methods.

## Results

|            Method |  rawData |      Mean |   Error |  StdDev | Serialized Size | Serialized [gzip] | Serialized [gzip_b64] |    Gen 0 |    Gen 1 |    Gen 2 | Allocated |
|------------------ |--------- |----------:|--------:|--------:|----------------:|------------------:|----------------------:|---------:|---------:|---------:|----------:|
|       FlatBuffers | TBS  [1] | 2,209.4us |  9.92us |  8.79us |        253.98KB |           80.13KB |              106.84KB |  78.1250 |  70.3125 |  70.3125 |  293.09KB |
|          Protobuf | TBS  [1] | 1,616.3us |  9.77us |  9.14us |        126.71KB |           42.35KB |               56.47KB | 197.2656 |  78.1250 |  39.0625 |  986.35KB |
|    NewtonsoftJson | TBS  [1] | 6,444.0us | 64.06us | 59.92us |        398.32KB |           59.24KB |               78.98KB | 593.7500 | 515.6250 | 328.1250 | 2779.38KB |
| MessagePackString | TBS  [1] | 1,104.3us | 10.34us |  8.63us |        409.49KB |           56.37KB |               75.16KB | 123.0469 | 123.0469 | 123.0469 |  819.77KB |
|    MessagePackInt | TBS  [1] |   778.4us |  6.64us |  6.21us |         96.93KB |           49.94KB |               66.59KB |  92.7734 |  60.5469 |  30.2734 |  507.21KB |
|      BinaryWriter | TBS  [1] |   647.1us |  3.85us |  3.41us |         78.14KB |           42.76KB |               57.02KB |  82.0313 |  41.0156 |  41.0156 |  334.64KB |
|       FlatBuffers | TBS [32] | 2,547.4us | 19.43us | 16.22us |        566.52KB |          329.66KB |              439.54KB | 144.5313 | 136.7188 | 136.7188 |  605.62KB |
|          Protobuf | TBS [32] | 1,664.9us | 16.57us | 15.50us |        429.48KB |          272.67KB |              363.56KB | 265.6250 | 212.8906 | 109.3750 | 1289.92KB |
|    NewtonsoftJson | TBS [32] | 7,211.8us | 45.65us | 42.70us |        701.08KB |          282.26KB |              376.34KB | 867.1875 | 789.0625 | 492.1875 | 4299.71KB |
| MessagePackString | TBS [32] | 1,149.0us | 22.33us | 28.24us |        722.02KB |          289.90KB |              386.53KB | 259.7656 | 207.0313 | 166.0156 | 1132.88KB |
|    MessagePackInt | TBS [32] | 1,004.6us |  5.71us |  5.06us |        409.46KB |          274.56KB |              366.08KB | 123.0469 | 123.0469 | 123.0469 |  819.76KB |
|      BinaryWriter | TBS [32] |   974.1us | 18.57us | 27.23us |        380.91KB |          266.44KB |              355.26KB | 341.7969 | 310.5469 | 309.5703 | 1406.09KB |

## Notes
- For `TBS [##]`, ## represents the length of the strings used.
- The Flatbuffers folder is directly copied from the official git repository (commit [261cf3b](https://github.com/google/flatbuffers/tree/261cf3b20473abdf95fc34da0827e4986f065c39)).
- flatc is copied from the [releases](https://github.com/google/flatbuffers/releases/tag/v1.12.0) section of the official git repository.
- protoc is copied from the [releases](https://github.com/protocolbuffers/protobuf/releases/tag/v3.15.6) section of the official git repository