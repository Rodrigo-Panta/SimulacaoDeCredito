using SimulacaoDeCredito.Domain.Entities;
using SimulacaoDeCredito.Domain.Strategies;

namespace SimulacaoDeCredito.Domain.Factories
{
    public static class SimulacaoFactory
    {
        public static Simulacao Criar(int prazo, decimal valor, Produto produto)
        {

            var strategies = new List<ICalculoTabelaStrategy>() { new SacStrategy(), new PriceStrategy(), };
            var simulacaoTabelas = strategies.Select(s => s.Calcular(valor, prazo, produto.PcTaxaJuros)).ToList();

            Simulacao simulacao = new Simulacao()
            {
                IdSimulacao = null,
                CodigoProduto = produto.CoProduto,
                DescricaoProduto = produto.NoProduto,
                Prazo = prazo,
                ValorDesejado = valor,
                TaxaJuros = produto.PcTaxaJuros,
                ResultadoSimulacao = simulacaoTabelas,
                DataCriacao = DateTime.Now
            };
            return simulacao;
        }
    }
}
