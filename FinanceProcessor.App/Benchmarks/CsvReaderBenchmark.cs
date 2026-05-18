using BenchmarkDotNet.Attributes;
using FinanceProcessor.App.Helpers;
using FinanceProcessor.Core.Services;

namespace FinanceProcessor.App.Benchmarks
{
    [MemoryDiagnoser]
    public class CsvReaderBenchmark
    {
        private readonly string _filePath = "benchmark_test.csv";

        [GlobalSetup]
        public void Setup()
        {
            var generator = new CsvGenerator();
            generator.Generate(_filePath, 100_000);
        }

        [Benchmark]
        public void LeituraComString()
        {
            var reader = new CsvReader();
            reader.ReadTransactions(_filePath);
        }

        [Benchmark]
        public void LeituraComSpan()
        {
            var readerOptimized = new CsvReaderOptimized();
            readerOptimized.ReadTransactions(_filePath);
        }
    }
}
