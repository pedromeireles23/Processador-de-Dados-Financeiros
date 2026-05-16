namespace FinanceProcessor.Core.Models{

public record Transaction(

   DateOnly Data,
   string Descricao ,
   decimal Valor,
   string Categoria,
   string Tipo
   );
}