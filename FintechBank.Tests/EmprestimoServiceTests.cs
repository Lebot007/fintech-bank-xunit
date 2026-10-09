using FintechBank.App;

namespace FintechBank.Tests;

/// <summary>
/// Suíte de testes unitários parametrizados para o EmprestimoService.
/// Desenvolvedor 2 (QA / Testes Unitários): Rafael Luiz Ferreira de Souza - RA 32511503.
/// Utiliza [Theory] + [InlineData] para cobrir os cenários exigidos na lista,
/// os casos-limite (boundary values) de score/margem e o caso de exceção.
/// </summary>
public class EmprestimoServiceTests
{
    private readonly EmprestimoService _service = new();

    /// <summary>
    /// Teste 1 (Cálculo de Parcela com Juros por Score): valida os três cenários
    /// exigidos na lista - taxas de 5%, 12% e 20% conforme o score de crédito.
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
    /// Teste 1 - Casos-limite (Boundary Value Analysis): valida as fronteiras exatas
    /// entre as faixas de score (700/699 e 500/499).
    /// </summary>
    [Theory]
    [InlineData(10000.00, 10, 700, 1050.00)] // Fronteira exata do score alto -> 5%
    [InlineData(10000.00, 10, 699, 1120.00)] // Um ponto abaixo do score alto -> 12%
    [InlineData(10000.00, 10, 500, 1120.00)] // Fronteira exata do score médio -> 12%
    [InlineData(10000.00, 10, 499, 1200.00)] // Um ponto abaixo do score médio -> 20%
    public void CalcularValorParcela_CasosLimite_DeveAplicarJurosCorreto(
        decimal valorTotal, int parcelas, int scoreCredito, decimal parcelaEsperada)
    {
        // Act
        decimal resultado = _service.CalcularValorParcela(valorTotal, parcelas, scoreCredito);

        // Assert
        Assert.Equal(parcelaEsperada, resultado);
    }

    /// <summary>
    /// Teste 1 - Caso de exceção: quantidade de parcelas inválida (zero ou negativa)
    /// deve lançar ArgumentException, evitando divisão por zero.
    /// </summary>
    [Theory]
    [InlineData(0)]  // Zero parcelas -> exceção
    [InlineData(-1)] // Parcelas negativas -> exceção
    public void CalcularValorParcela_ParcelasInvalidas_DeveLancarArgumentException(int parcelas)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(
            () => _service.CalcularValorParcela(10000.00m, parcelas, 800));
    }

    /// <summary>
    /// Teste 2 (Aprovação de Empréstimo por Margem Consignável): valida os dois
    /// cenários exigidos na lista - a parcela só pode comprometer até 30% da renda.
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

    /// <summary>
    /// Teste 2 - Casos-limite (Boundary Value Analysis): valida a fronteira exata
    /// da margem consignável de 30% (igual é aprovado; um centavo acima é recusado).
    /// </summary>
    [Theory]
    [InlineData(3000.00, 900.00, true)]  // Exatamente 30% da renda -> Aprovado
    [InlineData(3000.00, 900.01, false)] // Um centavo acima de 30% -> Recusado
    public void AprovarEmprestimo_CasosLimite_DeveRespeitarFronteiraDe30PorCento(
        decimal rendaMensal, decimal valorParcela, bool resultadoEsperado)
    {
        // Act
        bool resultado = _service.AprovarEmprestimo(rendaMensal, valorParcela);

        // Assert
        Assert.Equal(resultadoEsperado, resultado);
    }
}
