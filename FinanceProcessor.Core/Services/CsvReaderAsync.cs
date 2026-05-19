using FinanceProcessor.Core.Common;
using FinanceProcessor.Core.Models;

namespace FinanceProcessor.Core.Services
{
    public class CsvReaderAsync
    {
        public async Task<Result<List<Transaction>>> ReadTransactionAsync(
            string filePath,
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return Result<List<Transaction>>.Failure("O arquivo especificado não existe");
                }

                var transacoes = new List<Transaction>();
                bool primeiraLinha = true;

                await foreach (
                    string linha in File.ReadLinesAsync(filePath, cancellationToken)
                        .ConfigureAwait(false)
                )
                {
                    if (primeiraLinha)
                    {
                        primeiraLinha = false;
                        continue;
                    }
                    string[] campos = linha.Split(',');
                    var transacao = new Transaction(
                        DateOnly.Parse(campos[0]),
                        campos[1],
                        decimal.Parse(campos[2]),
                        campos[3],
                        campos[4]
                    );
                    transacoes.Add(transacao);
                }
                return Result<List<Transaction>>.Success(transacoes);
            }
            catch (OperationCanceledException)
            {
                return Result<List<Transaction>>.Failure("Processamento cancelado pelo usuário");
            }
            catch (Exception ex)
            {
                return Result<List<Transaction>>.Failure(
                    $"Erro ao processar o arquivo: {ex.Message}"
                );
            }
        }
    }
}
