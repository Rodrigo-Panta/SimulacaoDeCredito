using SimulacaoDeCredito.Domain.Entities;

namespace SimulacaoDeCredito.Domain.Repositories
{
    public interface IProdutoRepository
    {
        public Task<Produto?> ObterProdutoEnquadrado(decimal valorDesejado, int prazo);
    }
}
