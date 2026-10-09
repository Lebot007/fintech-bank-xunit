namespace FintechBank.App;

/// <summary>
/// Serviço com as regras de negócio da Conta Corrente (Dev 1 - Backend/Core).
/// </summary>
public class ContaCorrenteService
{
    /// <summary>
    /// Calcula a taxa de manutenção mensal da conta corrente.
    /// Regras:
    ///  - Saldo maior ou igual a R$ 5.000,00 OU cliente especial: isento (0.00m).
    ///  - Saldo entre R$ 1.000,00 e R$ 4.999,99 (não especial): R$ 12,50.
    ///  - Saldo menor que R$ 1.000,00 (não especial): R$ 25,00.
    /// </summary>
    /// <param name="saldo">Saldo atual da conta.</param>
    /// <param name="clienteEspecial">Indica se o cliente possui o benefício especial.</param>
    /// <returns>Valor da taxa de manutenção.</returns>
    public decimal CalcularTaxaManutencao(decimal saldo, bool clienteEspecial)
    {
        // Regra 1: isenção por saldo alto ou por ser cliente especial.
        if (saldo >= 5000.00m || clienteEspecial)
        {
            return 0.00m;
        }

        // Regra 2: saldo intermediário paga R$ 12,50.
        if (saldo >= 1000.00m && saldo <= 4999.99m)
        {
            return 12.50m;
        }

        // Regra 3: saldo baixo paga R$ 25,00.
        return 25.00m;
    }

    /// <summary>
    /// Verifica se um saque pode ser realizado, considerando o saldo
    /// disponível somado ao limite do cheque especial.
    /// </summary>
    /// <param name="saldo">Saldo atual da conta.</param>
    /// <param name="limiteChequeEspecial">Limite de cheque especial disponível.</param>
    /// <param name="valorSaque">Valor que o cliente deseja sacar.</param>
    /// <returns>True se o saque for permitido; caso contrário, False.</returns>
    public bool PodeRealizarSaque(decimal saldo, decimal limiteChequeEspecial, decimal valorSaque)
    {
        // O saque só é permitido se não ultrapassar o total (saldo + limite).
        return valorSaque <= saldo + limiteChequeEspecial;
    }
}
