﻿using BenchmarkDotNet.Running;

namespace Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            new BenchmarkSuite().QuickRun();
            BenchmarkRunner.Run<BenchmarkSuite>(Configuration.BenchmarkConfig);
        }
    }
}