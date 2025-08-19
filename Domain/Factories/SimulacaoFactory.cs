using SimulacaoDeCredito.Domain.Entities;
using SimulacaoDeCredito.Domain.Repositories;
using SimulacaoDeCredito.Domain.Strategies;

namespace SimulacaoDeCredito.Domain.Factories
{
    public static class SimulacaoFactory
    {
        public static Simulacao Criar(int prazo, decimal valor, Produto produto)
        {

            var strategies = new List<ICalculoTabelaStrategy>() { new PriceStrategy(), new SacStrategy() };
            var simulacaoTabelas = strategies.Select(s => s.Calcular(valor, prazo, produto.PcTaxaJuros)).ToList();

            var simulacao = new Simulacao()
            {
                codigoProduto = produto.CoProduto,
                descricaoProduto = produto.NoProduto,
                idSimulacao = 1,
                taxaJuros = produto.PcTaxaJuros,
                resultadoSimulacao = simulacaoTabelas
            };
            throw new NotImplementedException();
        }
    }
}
