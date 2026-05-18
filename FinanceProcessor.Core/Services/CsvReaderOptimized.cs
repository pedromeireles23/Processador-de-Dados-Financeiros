using FinanceProcessor.Core.Common;
using FinanceProcessor.Core.Models;

namespace FinanceProcessor.Core.Services
{
    public class CsvReaderOptimized
    {
        public Result<List<Transaction>> ReadTransactions(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return Result<List<Transaction>>.Failure(
                        $"O arquivo não foi encontrado no caminho: {filePath}"
                    );
                }

                List<Transaction> listTransacoes = new List<Transaction>();
                IEnumerable<string> lerLinhas = File.ReadLines(filePath).Skip(1);

                foreach (string linha in lerLinhas)
                {
                    ReadOnlySpan<char> converterElementos = linha.AsSpan();
                    Span<Range> fields = stackalloc Range[5];

                    int camposEncontrados = converterElementos.Split(fields, ',');

                    Transaction transacao = new Transaction(
                        DateOnly.Parse(converterElementos[fields[0]]),
                        converterElementos[fields[1]].ToString(),
                        decimal.Parse(converterElementos[fields[2]]),
                        converterElementos[fields[3]].ToString(),
                        converterElementos[fields[4]].ToString()
                    );
                    listTransacoes.Add(transacao);
                }
                return Result<List<Transaction>>.Success(listTransacoes);
            }
            catch (Exception ex)
            {
                return Result<List<Transaction>>.Failure(
                    $"Erro ao processar o arquivo CSV: {ex.Message}"
                );
            }
        }
    }
}
