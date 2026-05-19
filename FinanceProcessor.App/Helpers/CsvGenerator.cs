using System.Globalization;

namespace FinanceProcessor.App.Helpers
{
    public class CsvGenerator
    {
        string[] descricao =
        [
            "Café da Praça",
            "Academia Movimento",
            "Farmácia Vida",
            "Papelaria Estrela",
            "Mercado Belo Horizonte",
            "Pet Shop Amigo Fiel",
            "Loja Tech Center",
            "Restaurante Sabor Caseiro",
            "Açougue Central",
            "Padaria Pão Mineiro",
        ];
        string[] categoria =
        [
            "Lazer",
            "Fitness",
            "Saúde",
            "Educação",
            "Varejo",
            "Pets",
            "Tecnologia",
            "Alimentação",
            "Conveniência",
            "Serviços",
        ];
        string[] tipo = ["Débito", "Crédito"];

        public void Generate(string filePath, int count)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Data,Descricao,Valor,Categoria,Tipo");
                var random = new Random();
                DateTime dataFinal = DateTime.Now;
                DateTime dataInicial = dataFinal.AddYears(-1);
                int diasNoIntervalo = (dataFinal - dataInicial).Days;
                for (int i = 0; i < count; i++)
                {
                    DateTime dataAleatoria = dataInicial.AddDays(random.Next(diasNoIntervalo));
                    decimal numeroAleatorio =
                        (decimal)random.NextDouble() * (10001.0m - 1000.0m) + 1000.0m;
                    string numeroFormatado = numeroAleatorio.ToString(
                        "F2",
                        CultureInfo.InvariantCulture
                    );
                    writer.WriteLine(
                        $"{dataAleatoria:dd/MM/yyyy},{descricao[random.Next(descricao.Length)]},{numeroFormatado},{categoria[random.Next(categoria.Length)]},{tipo[random.Next(tipo.Length)]}"
                    );
                }
            }
        }
    }
}
