namespace FintechBank.App;

/// <summary>
/// Serviço com as regras de negócio de Empréstimo (Dev 1 - Backend/Core).
/// </summary>
public class EmprestimoService
{
    /// <summary>
    /// Calcula o valor da parcela individual de um empréstimo,
    /// aplicando a taxa de juros total conforme o score de crédito.
    /// Regras:
    ///  - Score >= 700: juros de 5%  (multiplica por 1.05m).
    ///  - Score entre 500 e 699: juros de 12% (multiplica por 1.12m).
    ///  - Score < 500: juros de 20% (multiplica por 1.20m).
    /// </summary>
    /// <param name="valorTotal">Valor total solicitado do empréstimo.</param>
    /// <param name="parcelas">Quantidade de parcelas.</param>
    /// <param name="scoreCredito">Score de crédito do cliente.</param>
    /// <returns>Valor da parcela individual (valor com juros / parcelas).</returns>
    public decimal CalcularValorParcela(decimal valorTotal, int parcelas, int scoreCredito)
    {
        if (parcelas <= 0)
        {
            throw new ArgumentException("A quantidade de parcelas deve ser maior que zero.", nameof(parcelas));
        }

        // Define o fator de juros conforme o score de crédito.
        decimal fatorJuros;
        if (scoreCredito >= 700)
        {
            fatorJuros = 1.05m; // 5% de juros
        }
        else if (scoreCredito >= 500)
        {
            fatorJuros = 1.12m; // 12% de juros
        }
        else
        {
            fatorJuros = 1.20m; // 20% de juros
        }

        decimal valorComJuros = valorTotal * fatorJuros;

        // Retorna a parcela individual.
        return valorComJuros / parcelas;
    }

    /// <summary>
    /// Aprova (ou não) o empréstimo com base na margem consignável:
    /// a parcela só pode comprometer no máximo 30% da renda mensal.
    /// </summary>
    /// <param name="rendaMensal">Renda mensal comprovada do cliente.</param>
    /// <param name="valorParcela">Valor da parcela do empréstimo.</param>
    /// <returns>True se aprovado (parcela <= 30% da renda); caso contrário, False.</returns>
    public bool AprovarEmprestimo(decimal rendaMensal, decimal valorParcela)
    {
        return valorParcela <= rendaMensal * 0.30m;
    }
}
