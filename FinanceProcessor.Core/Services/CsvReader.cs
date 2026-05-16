using FinanceProcessor.Core.Models;

namespace FinanceProcessor.Core.Services
{
  public class CsvReader
  {
    public List<Transaction> ReadTransactions(string filePath)

  

    {
      List<Transaction> listTransacoes = new List<Transaction>();
      string [] lerLinhas = File.ReadAllLines(filePath);
      for(int i = 1; i< lerLinhas.Length; i++)
      {
        string [] fields = lerLinhas[i].Split(',');
        listTransacoes.Add(new Transaction (DateOnly.Parse(fields[0]), fields[1], Decimal.Parse(fields[2]),fields[3],fields[4]));
      }
      return listTransacoes;
    }
  }
}