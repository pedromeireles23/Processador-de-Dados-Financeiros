# FinanceProcessor

Ferramenta de linha de comando em C# que lê arquivos CSV de extratos bancários, processa transações e gera relatórios de análise financeira. Construída com foco em aprendizado progressivo — a mesma funcionalidade implementada de formas diferentes, do jeito ingênuo ao otimizado, com métricas reais de performance.

---

## Funcionalidades

- Resumo mensal de receitas e despesas
- Ranking dos maiores gastos por categoria
- Saldo progressivo dia a dia
- Tempo de processamento e uso de memória
- Benchmarks comparativos entre abordagens de leitura

---

## Arquitetura

```
FinanceProcessor/
├── FinanceProcessor.Core/        # Regras de negócio e modelos
│   ├── Models/
│   │   └── Transaction.cs        # Record imutável de transação
│   ├── Services/
│   │   ├── CsvReader.cs          # Leitura com string (baseline)
│   │   ├── CsvReaderOptimized.cs # Leitura com Span<char>
│   │   └── CsvReaderAsync.cs     # Leitura assíncrona
│   ├── Processing/
│   │   └── TransactionProcessor.cs # Processamento e relatórios
│   └── Common/
│       └── Result.cs             # Padrão Result<T>
├── FinanceProcessor.App/         # Entrada da aplicação
│   ├── Helpers/
│   │   └── CsvGenerator.cs       # Gerador de CSV de teste
│   └── Benchmarks/
│       └── CsvReaderBenchmark.cs # Benchmarks com BenchmarkDotNet
└── FinanceProcessor.Tests/       # Testes unitários
```

---

## Conceitos Aplicados

### Records e Imutabilidade
Transações modeladas como `record` com Primary Constructor — igualdade por valor, propriedades `init`-only, impossibilidade de mutação após criação.

### Padrão Result\<T\>
Tratamento de erros sem exceções. Erros esperados (arquivo não encontrado, linha inválida) retornam `Result<T>.Failure()` em vez de lançar exceções — tornando o contrato do método explícito e o fluxo de erro visível em tempo de compilação.

### Pattern Matching e Switch Expressions
Switch expressions modernas para classificar transações por tipo e categoria, eliminando cadeias de `if/else` e tornando os casos exaustivos verificáveis pelo compilador.

### LINQ com Execução Adiada
Processamento de coleções com `GroupBy`, `Sum`, `OrderByDescending`, `Take` e `ToDictionary`. Uso consciente de operadores de materialização para controlar quando a execução acontece.

### Span\<char\> e Zero-Allocation
Leitura otimizada que opera diretamente na memória da string original sem criar strings intermediárias. O `MemoryExtensions.Split` preenche um array de `Range` com índices de início e fim de cada campo — zero cópias, zero alocações temporárias.

### Programação Assíncrona Real
Além do `async/await` básico: `IAsyncEnumerable<T>` para processar itens em pipeline conforme chegam, `CancellationToken` para cancelamento cooperativo, e `ConfigureAwait(false)` para evitar captura desnecessária de contexto em código de infraestrutura.

---

##  Resultados do Benchmark

Medido com BenchmarkDotNet em modo Release, AMD Ryzen 5 2600, .NET 10, 100.000 transações:

| Implementação    | Tempo     | Memória   |
|-----------------|----------:|----------:|
| `CsvReader`      | 158.5 ms  | 51.18 MB  |
| `CsvReaderOptimized` | 121.3 ms | 40.02 MB |

A versão com `Span<char>` foi **24% mais rápida** e alocou **22% menos memória** — diferença direta da eliminação de ~500k strings temporárias criadas pelo `Split(',')` na versão ingênua.

---

##  Como Executar

**Relatórios no terminal:**
```bash
dotnet run --project FinanceProcessor.App
```

**Benchmarks:**
```bash
dotnet run --project FinanceProcessor.App -c Release
```
> Descomente as linhas do `BenchmarkRunner` no `Program.cs` antes de rodar os benchmarks.

---

##  Tecnologias

- **.NET 10 / C#**
- **BenchmarkDotNet** — benchmarks de performance e memória
- **Span\<T\> / ReadOnlySpan\<char\>** — manipulação de memória sem alocação
- **IAsyncEnumerable\<T\>** — streaming assíncrono de dados
