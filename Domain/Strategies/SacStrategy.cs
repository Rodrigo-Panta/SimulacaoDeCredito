namespace SimulacaoDeCredito.Domain.Strategies
{
    public class SacStrategy : ICalculoTabelaStrategy
    {
        public SimulacaoTabela Calcular(decimal valor, int prazo, decimal taxa)
        {
            decimal saldoDevedor = valor;
            decimal amortizacao = Math.Round(valor / prazo, 2, MidpointRounding.AwayFromZero);
            var parcelas = new List<SimulacaoParcela>();
            for (int i = 1; i <= prazo; i++)
            {
                decimal juros = Math.Round(saldoDevedor * taxa, 2, MidpointRounding.AwayFromZero);
                decimal prestacao = amortizacao + juros;
                saldoDevedor -= amortizacao;

                parcelas.Add(new SimulacaoParcela()
                {
                    numero = i,
                    valorAmortizacao = amortizacao,
                    valorJuros = juros,
                    valorPrestacao = prestacao
                });
            }

            return new SimulacaoTabela()
            {
                tipo = "SAC",
                parcelas = parcelas
            };
        }

    }
}
