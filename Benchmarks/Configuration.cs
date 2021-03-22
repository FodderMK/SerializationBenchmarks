using BenchmarkDotNet.Configs;

namespace Benchmark
{
    public static class Configuration
    {
        // public const int Rows = 1000000;
        public const int SmallStringLength = 1;
        public const int LargeStringLength = 32;
        public const int Rows = 10000;

        public static IConfig BenchmarkConfig =>
            ManualConfig.Create(DefaultConfig.Instance)
                .WithOption(ConfigOptions.DisableOptimizationsValidator, true)
                .WithOption(ConfigOptions.DisableLogFile, true)
                .AddColumn(new SerializedSize())
                .AddColumn(new GzipSize())
                .AddColumn(new GzipBase64Size());

    }
}