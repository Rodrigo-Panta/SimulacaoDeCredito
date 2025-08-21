namespace SimulacaoDeCredito.Domain.Strategies
{
    public class PriceStrategy : ICalculoTabelaStrategy
    {
        public SimulacaoTabela Calcular(decimal valor, int prazo, decimal taxa)
        {
            var parcelas = new List<SimulacaoParcela>();
            decimal prestacao = Math.Round(valor * taxa /
                                            (decimal)(1 - (decimal)Math.Pow((double)(1 + taxa), -prazo)), 2, MidpointRounding.AwayFromZero);
            decimal saldoDevedor = valor;

            for (int i = 1; i <= prazo; i++)
            {
                decimal juros = Math.Round(saldoDevedor * taxa, 2, MidpointRounding.AwayFromZero);
                decimal amortizacao = prestacao - juros;
                saldoDevedor -= amortizacao;
                saldoDevedor = Math.Round(saldoDevedor, 2, MidpointRounding.AwayFromZero);

                parcelas.Add(new SimulacaoParcela()
                {
                    Numero = i,
                    ValorAmortizacao = amortizacao,
                    ValorJuros = juros,
                    ValorPrestacao = prestacao
                });
            }

            return new SimulacaoTabela()
            {
                Tipo = "PRICE",
                Parcelas = parcelas,
                ValorTotalParcelas = Math.Round(prestacao * prazo, 2, MidpointRounding.AwayFromZero)
            };
        }

    }
}
