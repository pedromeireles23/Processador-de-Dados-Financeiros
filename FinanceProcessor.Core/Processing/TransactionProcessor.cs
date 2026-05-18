using FinanceProcessor.Core.Models;

namespace FinanceProcessor.Core.Processing
{
    public class TransactionProcessor
    {
        private decimal GetMultiplier(string tipo) =>
            tipo switch
            {
                "Crédito" => 1m,
                "Débito" => -1m,
                _ => 0m,
            };

        private decimal SaldoCalculado(Transaction transaction)
        {
            decimal saldoCalculado = transaction.Valor * GetMultiplier(transaction.Tipo);
            return saldoCalculado;
        }

        public Dictionary<string, decimal> GetMonthlySummary(IEnumerable<Transaction> transactions)
        {
            if (transactions == null)
                return new Dictionary<string, decimal>();

            return transactions
                .GroupBy(t => t.Data.ToString("yyyy-MM"))
                .ToDictionary(grupo => grupo.Key, grupo => grupo.Sum(t => SaldoCalculado(t)));
        }

        public Dictionary<string, decimal> GetTopExpenses(IEnumerable<Transaction> transactions)
        {
            if (transactions == null)
                return new Dictionary<string, decimal>();

            return transactions
                .Where(t => t.Tipo == "Débito")
                .GroupBy(t => t.Categoria)
                .Select(grupo => new { Categoria = grupo.Key, Total = grupo.Sum(t => t.Valor) })
                .OrderByDescending(x => x.Total)
                .Take(10)
                .ToDictionary(x => x.Categoria, x => x.Total);
        }

        public Dictionary<DateOnly, decimal> GetDailyBalance(IEnumerable<Transaction> transactions)
        {
            if (transactions == null)
                return new Dictionary<DateOnly, decimal>();

            var saldoIsoladoPorDia = transactions
                .GroupBy(t => t.Data)
                .Select(grupo => new
                {
                    Data = grupo.Key,
                    SaldoDoDia = grupo.Sum(t => SaldoCalculado(t)),
                })
                .OrderBy(x => x.Data)
                .ToList();

            var saldoAcumulado = new Dictionary<DateOnly, decimal>();
            decimal acumulador = 0m;

            foreach (var dia in saldoIsoladoPorDia)
            {
                acumulador += dia.SaldoDoDia;
                saldoAcumulado.Add(dia.Data, acumulador);
            }
            return saldoAcumulado;
        }
    }
}
