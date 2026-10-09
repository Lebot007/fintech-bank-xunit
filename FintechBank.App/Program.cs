using FintechBank.App;

// Ponto de entrada da aplicação FintechBank (demonstração das regras de negócio).
var contaCorrente = new ContaCorrenteService();
var emprestimo = new EmprestimoService();

Console.WriteLine("=== FintechBank - Regras de Negócio ===");
Console.WriteLine();

// --- Conta Corrente ---
Console.WriteLine("[Conta Corrente] Taxa de manutenção (saldo R$ 6.000,00, comum): " +
    contaCorrente.CalcularTaxaManutencao(6000.00m, false).ToString("C"));
Console.WriteLine("[Conta Corrente] Taxa de manutenção (saldo R$ 2.500,00, comum): " +
    contaCorrente.CalcularTaxaManutencao(2500.00m, false).ToString("C"));
Console.WriteLine("[Conta Corrente] Taxa de manutenção (saldo R$ 300,00, comum): " +
    contaCorrente.CalcularTaxaManutencao(300.00m, false).ToString("C"));
Console.WriteLine("[Conta Corrente] Saque de R$ 1.200,00 (saldo R$ 1.000,00 + limite R$ 500,00): " +
    (contaCorrente.PodeRealizarSaque(1000.00m, 500.00m, 1200.00m) ? "PERMITIDO" : "BLOQUEADO"));
Console.WriteLine();

// --- Empréstimo ---
Console.WriteLine("[Empréstimo] Parcela (R$ 10.000,00 em 10x, score 800): " +
    emprestimo.CalcularValorParcela(10000.00m, 10, 800).ToString("C"));
Console.WriteLine("[Empréstimo] Parcela (R$ 10.000,00 em 10x, score 600): " +
    emprestimo.CalcularValorParcela(10000.00m, 10, 600).ToString("C"));
Console.WriteLine("[Empréstimo] Parcela (R$ 10.000,00 em 10x, score 400): " +
    emprestimo.CalcularValorParcela(10000.00m, 10, 400).ToString("C"));
Console.WriteLine("[Empréstimo] Aprovação (renda R$ 5.000,00, parcela R$ 1.500,00): " +
    (emprestimo.AprovarEmprestimo(5000.00m, 1500.00m) ? "APROVADO" : "RECUSADO"));
Console.WriteLine("[Empréstimo] Aprovação (renda R$ 5.000,00, parcela R$ 1.600,00): " +
    (emprestimo.AprovarEmprestimo(5000.00m, 1600.00m) ? "APROVADO" : "RECUSADO"));
