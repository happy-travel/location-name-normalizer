using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;

namespace Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ManualConfig()
                .AddLogger(newLoggers: new ConsoleLogger())
                .AddDiagnoser(MemoryDiagnoser.Default)
                .WithOptions(ConfigOptions.DisableOptimizationsValidator);

            var summary = BenchmarkRunner.Run<NormalizersBenchmark>(config);
            System.Console.WriteLine(summary);
        }
    }
}
