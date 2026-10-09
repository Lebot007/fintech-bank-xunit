using FintechBank.App;

namespace FintechBank.Tests;

/// <summary>
/// Suíte de testes unitários parametrizados para o ContaCorrenteService.
/// Desenvolvedor 2 (QA / Testes Unitários): Rafael Luiz Ferreira de Souza - RA 32511503.
/// Utiliza [Theory] + [InlineData] para cobrir os cenários exigidos na lista
/// e também os casos-limite (boundary values) de cada regra de negócio.
/// </summary>
public class ContaCorrenteServiceTests
{
    private readonly ContaCorrenteService _service = new();

    /// <summary>
    /// Teste 1 (Taxa de Manutenção): valida os quatro cenários exigidos na lista
    /// (isento por saldo alto, isento por cliente especial, taxa intermediária e taxa baixa).
    /// </summary>
    [Theory]
    [InlineData(6000.00, false, 0.00)]  // Saldo alto -> Isento
    [InlineData(500.00, true, 0.00)]    // Cliente Especial -> Isento
    [InlineData(2500.00, false, 12.50)] // Saldo intermediário -> R$ 12,50
    [InlineData(300.00, false, 25.00)]  // Saldo baixo -> R$ 25,00
    public void CalcularTaxaManutencao_DeveRetornarTaxaCorreta(
        decimal saldo, bool clienteEspecial, decimal taxaEsperada)
    {
        // Act
        decimal resultado = _service.CalcularTaxaManutencao(saldo, clienteEspecial);

        // Assert
        Assert.Equal(taxaEsperada, resultado);
    }

    /// <summary>
    /// Teste 1 - Casos-limite (Boundary Value Analysis): valida as fronteiras exatas
    /// entre as faixas de saldo (R$ 5.000,00 / R$ 4.999,99 e R$ 1.000,00 / R$ 999,99).
    /// </summary>
    [Theory]
    [InlineData(5000.00, false, 0.00)]  // Fronteira exata da isenção -> Isento
    [InlineData(4999.99, false, 12.50)] // Um centavo abaixo da isenção -> R$ 12,50
    [InlineData(1000.00, false, 12.50)] // Fronteira exata da taxa intermediária -> R$ 12,50
    [InlineData(999.99, false, 25.00)]  // Um centavo abaixo da intermediária -> R$ 25,00
    public void CalcularTaxaManutencao_CasosLimite_DeveRetornarTaxaCorreta(
        decimal saldo, bool clienteEspecial, decimal taxaEsperada)
    {
        // Act
        decimal resultado = _service.CalcularTaxaManutencao(saldo, clienteEspecial);

        // Assert
        Assert.Equal(taxaEsperada, resultado);
    }

    /// <summary>
    /// Teste 2 (Validação de Saque): valida os dois cenários exigidos na lista
    /// (saque dentro do limite total e saque que excede saldo + cheque especial).
    /// </summary>
    [Theory]
    [InlineData(1000.00, 500.00, 1200.00, true)]  // Dentro do limite total -> true
    [InlineData(1000.00, 500.00, 1600.00, false)] // Excede saldo + limite -> false
    public void PodeRealizarSaque_DeveValidarLimiteTotal(
        decimal saldo, decimal limiteChequeEspecial, decimal valorSaque, bool resultadoEsperado)
    {
        // Act
        bool resultado = _service.PodeRealizarSaque(saldo, limiteChequeEspecial, valorSaque);

        // Assert
        Assert.Equal(resultadoEsperado, resultado);
    }

    /// <summary>
    /// Teste 2 - Casos-limite (Boundary Value Analysis): valida a fronteira exata
    /// do limite total (saque igual a saldo + limite é permitido; um centavo acima não).
    /// </summary>
    [Theory]
    [InlineData(1000.00, 500.00, 1500.00, true)]  // Exatamente saldo + limite -> true
    [InlineData(1000.00, 500.00, 1500.01, false)] // Um centavo acima do limite -> false
    public void PodeRealizarSaque_CasosLimite_DeveValidarFronteiraDoLimite(
        decimal saldo, decimal limiteChequeEspecial, decimal valorSaque, bool resultadoEsperado)
    {
        // Act
        bool resultado = _service.PodeRealizarSaque(saldo, limiteChequeEspecial, valorSaque);

        // Assert
        Assert.Equal(resultadoEsperado, resultado);
    }
}
