using FintechBank.App;

namespace FintechBank.Tests;

/// <summary>
/// Suíte de testes unitários parametrizados para o EmprestimoService (Dev 2 - QA).
/// Utiliza [Theory] + [InlineData] para cobrir todos os cenários das regras de negócio.
/// </summary>
public class EmprestimoServiceTests
{
    private readonly EmprestimoService _service = new();

    /// <summary>
    /// Teste 1 (Cálculo de Parcela com Juros por Score): valida a aplicação
    /// das taxas de 5%, 12% e 20% conforme o score de crédito.
    /// </summary>
    [Theory]
    [InlineData(10000.00, 10, 800, 1050.00)] // Score alto: 5% de juros  -> R$ 10.500 / 10 = R$ 1.050,00
    [InlineData(10000.00, 10, 600, 1120.00)] // Score médio: 12% de juros -> R$ 11.200 / 10 = R$ 1.120,00
    [InlineData(10000.00, 10, 400, 1200.00)] // Score baixo: 20% de juros -> R$ 12.000 / 10 = R$ 1.200,00
    public void CalcularValorParcela_DeveAplicarJurosPorScore(
        decimal valorTotal, int parcelas, int scoreCredito, decimal parcelaEsperada)
    {
        // Act
        decimal resultado = _service.CalcularValorParcela(valorTotal, parcelas, scoreCredito);

        // Assert
        Assert.Equal(parcelaEsperada, resultado);
    }

    /// <summary>
    /// Teste 2 (Aprovação de Empréstimo por Margem Consignável): a parcela
    /// só pode comprometer no máximo 30% da renda mensal.
    /// </summary>
    [Theory]
    [InlineData(5000.00, 1500.00, true)]  // Parcela = 30% da renda -> Aprovado
    [InlineData(5000.00, 1600.00, false)] // Parcela > 30% da renda -> Recusado
    public void AprovarEmprestimo_DeveRespeitarMargemConsignavel(
        decimal rendaMensal, decimal valorParcela, bool resultadoEsperado)
    {
        // Act
        bool resultado = _service.AprovarEmprestimo(rendaMensal, valorParcela);

        // Assert
        Assert.Equal(resultadoEsperado, resultado);
    }
}
