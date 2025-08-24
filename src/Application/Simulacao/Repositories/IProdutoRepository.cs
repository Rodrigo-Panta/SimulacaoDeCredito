using SimulacaoDeCredito.Domain.Entities;

namespace SimulacaoDeCredito.Domain.Repositories
{
    public interface IProdutoRepository
    {
        public Task<Produto?> ObterProdutoPorPrazo(int prazo);
    }
}
