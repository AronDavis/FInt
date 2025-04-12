using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters.Json;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace Benchmarks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                args = ["--filter", "*"];
            }

            ManualConfig config = DefaultConfig.Instance.AddExporter(JsonExporter.Brief)
                .AddDiagnoser(MemoryDiagnoser.Default);

            IEnumerable<Summary> summaries = BenchmarkSwitcher
                .FromAssembly(typeof(Program).Assembly)
                .Run(args, config);
        }
    }
}