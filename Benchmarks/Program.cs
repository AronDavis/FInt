using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters.Json;
using BenchmarkDotNet.Running;

namespace Benchmarks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = DefaultConfig.Instance.AddExporter(JsonExporter.Full);

            var results = BenchmarkRunner.Run<DivisionBenchmarks>(config);
        }
    }
}