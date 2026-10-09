using FintechBank.App;

namespace FintechBank.Tests;

/// <summary>
/// Suíte de testes unitários parametrizados para o ContaCorrenteService (Dev 2 - QA).
/// Utiliza [Theory] + [InlineData] para cobrir todos os cenários das regras de negócio.
/// </summary>
public class ContaCorrenteServiceTests
{
    private readonly ContaCorrenteService _service = new();

    /// <summary>
    /// Teste 1 (Taxa de Manutenção): valida os três cenários da regra
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
    /// Teste 2 (Validação de Saque): valida se o saque respeita o limite total
    /// (saldo + cheque especial).
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
}
