using System;
using System.Diagnostics;
using BenchmarkDotNet.Running;
using FinanceProcessor.App.Benchmarks;
using FinanceProcessor.App.Helpers;
using FinanceProcessor.Core.Common;
using FinanceProcessor.Core.Processing;
using FinanceProcessor.Core.Services;
using Transaction = FinanceProcessor.Core.Models.Transaction;

namespace FinanceProcessor.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Aplicação FinanceProcessor iniciada.");

            //=========================================================================
            Console.WriteLine("Iniciando os testes de Benchmark...");
            var summary = BenchmarkRunner.Run<CsvReaderBenchmark>();
            //=========================================================================

            Console.WriteLine("Processamento finalizado. Pressione qualquer tecla para sair.");
            Console.ReadKey();

            // const string fileName = "transacoes.csv";
            // const int totalLines = 100_000;

            // var generator = new CsvGenerator();
            // var reader = new CsvReader();
            // var processor = new TransactionProcessor();

            // System.Console.WriteLine($"Gerando {totalLines:N0} transações em '{fileName}' ...");
            // generator.Generate(fileName, totalLines);
            // System.Console.WriteLine("Arquivo gerado com sucesso. \n");

            // long memoryBefore = GC.GetTotalMemory(false);

            // Stopwatch stopwatch = Stopwatch.StartNew();
            // Result<List<Transaction>> readResult = reader.ReadTransactions(fileName);
            // if (!readResult.IsSuccess)
            // {
            //     Console.ForegroundColor = ConsoleColor.Red;
            //     // Ajustado para .Error conforme sua classe Result<T>
            //     Console.WriteLine($"Erro ao ler o arquivo CSV: {readResult.Error}");
            //     Console.ResetColor();
            //     return;
            // }

            // Captura a lista de transações vinda da propriedade .Value (que é um List<Transaction>?)
            // O operador '!' avisa o compilador que sabemos que não é nulo devido ao IsSuccess
            // List<Transaction> transactions = readResult.Value!;

            // 5. Processar os três relatórios usando as instâncias e métodos reais do seu Processor
            // Dictionary<string, decimal> monthlySummary = processor.GetMonthlySummary(transactions);
            // Dictionary<string, decimal> topExpenses = processor.GetTopExpenses(transactions);
            // Dictionary<DateOnly, decimal> dailyBalance = processor.GetDailyBalance(transactions);

            // 6. Parar o Stopwatch e medir a memória depois
            // stopwatch.Stop();
            // long memoryAfter = GC.GetTotalMemory(false);

            // 7. Exibir os relatórios com separadores no terminal
            // Console.WriteLine("==================================================");
            // Console.WriteLine("               RELATÓRIOS FINANCEIROS             ");
            // Console.WriteLine("==================================================");

            // Relatório 1: Resumo Mensal (Saldo calculado por Mês)
            // Console.WriteLine("\n--- RESUMO MENSAL (SALDO POR MÊS) ---");
            // foreach (var item in monthlySummary)
            // {
            //     Console.WriteLine($"Mês {item.Key}: {item.Value:C}");
            // }

            // Relatório 2: Top 5 Categorias com Maior Gasto (.Take(5) sobre os pares Chave/Valor)
            // Console.WriteLine("\n--- TOP 5 CATEGORIAS (MAIOR GASTO) ---");
            // foreach (var item in topExpenses.Take(5))
            // {
            //     Console.WriteLine($"{item.Key}: {item.Value:C}");
            // }

            // Relatório 3: Saldo Diário (Primeiros 5 e Últimos 5 dias usando DateOnly)
            // Console.WriteLine("\n--- SALDO DIÁRIO (PRIMEIROS 5 DIAS) ---");
            // foreach (var day in dailyBalance.Take(5))
            // {
            //     Console.WriteLine($"{day.Key:dd/MM/yyyy}: {day.Value:C}");
            // }

            // Console.WriteLine("...");

            // Console.WriteLine("--- SALDO DIÁRIO (ÚLTIMOS 5 DIAS) ---");
            // foreach (var day in dailyBalance.TakeLast(5))
            // {
            //     Console.WriteLine($"{day.Key:dd/MM/yyyy}: {day.Value:C}");
            // }

            // Console.WriteLine("==================================================");

            // 8. Exibir métricas de tempo e memória no final
            // double memoryUsedMb = (memoryAfter - memoryBefore) / (1024.0 * 1024.0);

            // Console.WriteLine($"\nTempo de Processamento: {stopwatch.ElapsedMilliseconds} ms");
            // Console.WriteLine($"Memória Alocada no Processo: {memoryUsedMb:F2} MB");
        }
    }
}
