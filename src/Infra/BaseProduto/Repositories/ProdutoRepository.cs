using Microsoft.EntityFrameworkCore;
using SimulacaoDeCredito.Domain.Entities;
using SimulacaoDeCredito.Domain.Repositories;
using SimulacaoDeCredito.Infrastructure.BaseProduto.Persistence;

namespace SimulacaoDeCredito.Infra.Repositories
{

    public class ProdutoRepository : IProdutoRepository
    {
        private readonly BaseProdutosDbContext _context;

        public ProdutoRepository(BaseProdutosDbContext context)
        {
            _context = context;
        }

        public async Task<Produto?> ObterProdutoPorPrazo(int prazo)
        {

            var resultado = await _context.Produtos
                    .Where(p => p.NuMinimoMeses <= prazo && (p.NuMaximoMeses >= prazo || p.NuMaximoMeses == null))
                    .FirstOrDefaultAsync();
            return resultado;
        }
    }
}
