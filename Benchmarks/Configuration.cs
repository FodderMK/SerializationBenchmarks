using BenchmarkDotNet.Configs;

namespace Benchmark
{
    public static class Configuration
    {
        // public const int Rows = 1000000;
        public const int Rows = 10000;
        public const string DefaultString = "This is a string value";

        public static IConfig BenchmarkConfig =>
            ManualConfig.Create(DefaultConfig.Instance)
                .WithOption(ConfigOptions.DisableOptimizationsValidator, true)
                .WithOption(ConfigOptions.DisableLogFile, true)
                .AddColumn(new SerializedSize())
                .AddColumn(new GzipSize())
                .AddColumn(new GzipBase64Size());

    }
}