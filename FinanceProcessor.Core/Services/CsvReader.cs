using FinanceProcessor.Core.Common;
using FinanceProcessor.Core.Models;

namespace FinanceProcessor.Core.Services
{
    public class CsvReader
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
                string[] lerLinhas = File.ReadAllLines(filePath);
                for (int i = 1; i < lerLinhas.Length; i++)
                {
                    string[] fields = lerLinhas[i].Split(',');
                    listTransacoes.Add(
                        new Transaction(
                            DateOnly.Parse(fields[0]),
                            fields[1],
                            Decimal.Parse(fields[2]),
                            fields[3],
                            fields[4]
                        )
                    );
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
