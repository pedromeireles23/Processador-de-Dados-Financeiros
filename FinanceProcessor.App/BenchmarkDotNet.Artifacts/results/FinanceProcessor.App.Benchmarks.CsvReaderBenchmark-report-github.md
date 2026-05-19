```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
AMD Ryzen 5 2600 3.40GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 10.0.200
  [Host]     : .NET 10.0.4 (10.0.4, 10.0.426.12010), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.4 (10.0.4, 10.0.426.12010), X64 RyuJIT x86-64-v3


```
| Method           | Mean     | Error   | StdDev  | Gen0      | Gen1      | Gen2      | Allocated |
|----------------- |---------:|--------:|--------:|----------:|----------:|----------:|----------:|
| LeituraComString | 158.5 ms | 3.13 ms | 5.31 ms | 9000.0000 | 5000.0000 | 1250.0000 |  51.18 MB |
| LeituraComSpan   | 121.3 ms | 2.39 ms | 2.84 ms | 7333.3333 | 4000.0000 | 1333.3333 |  40.02 MB |
