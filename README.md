# 💰 FintechBank

Aplicação de console em **.NET 10** com o *core* de regras de negócio de um banco digital, desenvolvida para a disciplina de **Garantia da Qualidade de Software** (Gestão e Qualidade de Software — Prof. Daniel Henrique Matos de Paiva), com testes unitários **parametrizados** em **xUnit**.

## 👥 Equipe

| Aluno | RA | Papel | GitHub |
|---|---|---|---|
| João Vitor Alves Rodrigues | 32513480 | **Dev 1** (Backend/Core) + **Dev 3** (Docs/DevOps) | [@Lebot007](https://github.com/Lebot007) |
| Rafael Luiz Ferreira de Souza | 32511503 | **Dev 2** (QA/Testes Unitários) | — |

## ⚙️ Requisitos Técnicos

- [.NET 10 SDK](https://dotnet.microsoft.com/pt-br/download/dotnet/10.0) · Git · Terminal (PowerShell, Bash ou VS Code)

## 🧮 Regras de Negócio

### `ContaCorrenteService`
- `CalcularTaxaManutencao(saldo, clienteEspecial)` → isento (`0.00m`) se saldo ≥ R$ 5.000,00 **OU** cliente especial; `12.50m` se saldo entre R$ 1.000,00 e R$ 4.999,99; `25.00m` se saldo < R$ 1.000,00.
- `PodeRealizarSaque(saldo, limiteChequeEspecial, valorSaque)` → `true` apenas se `valorSaque <= saldo + limiteChequeEspecial`.

### `EmprestimoService`
- `CalcularValorParcela(valorTotal, parcelas, scoreCredito)` → juros totais por score: ≥ 700 = 5% (`×1.05m`); 500–699 = 12% (`×1.12m`); < 500 = 20% (`×1.20m`). Parcela = `valorComJuros / parcelas`.
- `AprovarEmprestimo(rendaMensal, valorParcela)` → `true` apenas se `valorParcela <= rendaMensal * 0.30m` (margem consignável de 30%).

## 🚀 Como Clonar e Compilar

    git clone https://github.com/Lebot007/fintech-bank-xunit.git
    cd fintech-bank-xunit
    dotnet build
    dotnet run --project FintechBank.App

## ✅ Como Executar os Testes Unitários

    dotnet test

Resultado esperado: `Passed! - Failed: 0, Passed: 11, Skipped: 0, Total: 11`

## 🧪 Testes Parametrizados (`[Theory]` / `[InlineData]`)

`[Theory]` marca um método de teste como uma *teoria*: ele vale para **vários conjuntos de dados**, e o xUnit o executa uma vez para cada conjunto. Cada `[InlineData(...)]` fornece um conjunto (entradas + resultado esperado) e aparece no relatório como um **teste independente**, sem fontes externas de dados.

Assim, um único método cobre todos os cenários de cada regra: 4 + 2 casos em `ContaCorrenteServiceTests` e 3 + 2 em `EmprestimoServiceTests` = **11 testes**.

    [Theory]
    [InlineData(6000.00, false, 0.00)]  // Saldo alto -> isento
    [InlineData(2500.00, false, 12.50)] // Saldo intermediário -> R$ 12,50
    public void CalcularTaxaManutencao_DeveRetornarTaxaCorreta(
        decimal saldo, bool clienteEspecial, decimal taxaEsperada)
    {
        decimal resultado = new ContaCorrenteService().CalcularTaxaManutencao(saldo, clienteEspecial);
        Assert.Equal(taxaEsperada, resultado);
    }

## 📄 Licença

Este projeto está licenciado sob a [MIT License](LICENSE).
