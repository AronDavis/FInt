using BenchmarkDotNet.Attributes;

namespace Benchmarks
{
    public class MultiplicationBenchmarks
    {
        private FInt _a = new FInt(100);
        private FInt _b = new FInt(200);

        private long _al = 100;
        private long _bl = 200;

        [Benchmark]
        public long MultiplicationLong() => _al * _bl;

        [Benchmark]
        public FInt Multiplication() => _a * _b;
    }
}
