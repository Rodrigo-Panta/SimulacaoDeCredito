using SimulacaoDeCredito.Domain.Entities;
using SimulacaoDeCredito.Domain.Repositories;
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
                codigoProduto = produto.CoProduto,
                descricaoProduto = produto.NoProduto,
                idSimulacao = 1,
                taxaJuros = produto.PcTaxaJuros,
                resultadoSimulacao = simulacaoTabelas
            };
            return simulacao;
        }
    }
}
