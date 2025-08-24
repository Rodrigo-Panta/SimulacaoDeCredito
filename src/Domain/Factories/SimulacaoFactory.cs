using SimulacaoDeCredito.Domain.Entities;
using SimulacaoDeCredito.Domain.Exceptions;
using SimulacaoDeCredito.Domain.Strategies;

namespace SimulacaoDeCredito.Domain.Factories
{
    public static class SimulacaoFactory
    {
        public static Simulacao Criar(int prazo, decimal valor, Produto? produto)
        {
            if (produto == null)
            {
                throw new DomainException("Não foi encontrado um produto para o prazo informado.");
            }

            if (valor < produto.VrMinimo)
            {
                throw new DomainException($"Para o prazo informado, o valor desejado deve ser de no mínimo R${produto.VrMinimo}.");
            }

            if (produto.VrMaximo.HasValue && valor > produto.VrMaximo.Value)
            {
                throw new DomainException($"Para o prazo informado, o valor desejado deve ser de no máximo R${produto.VrMaximo.Value}.");
            }

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
