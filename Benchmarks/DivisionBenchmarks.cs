using BenchmarkDotNet.Attributes;

namespace Benchmarks
{
    public class DivisionBenchmarks
    {
        private FInt _a = new FInt(100);
        private FInt _b = new FInt(200);

        private long _al = 100;
        private long _bl = 200;

        [Benchmark(Baseline = true)]
        public long DivisionLong() => _al / _bl;

        [Benchmark()]
        public FInt Division() => _a / _b;
    }
}
