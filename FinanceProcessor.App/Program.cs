using System;

// using FinanceProcessor.App.Benchmarks;
// using BenchmarkDotNet.Running;

namespace FinanceProcessor.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Aplicação FinanceProcessor iniciada.");

            // =========================================================================
            // Console.WriteLine("Iniciando os testes de Benchmark...");
            // var summary = BenchmarkRunner.Run<CsvReaderBenchmark>();
            // =========================================================================

            Console.WriteLine("Processamento finalizado. Pressione qualquer tecla para sair.");
            Console.ReadKey();
        }
    }
}
