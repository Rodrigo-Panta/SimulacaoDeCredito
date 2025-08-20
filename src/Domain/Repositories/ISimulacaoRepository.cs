using SimulacaoDeCredito.Domain.Entities;
using SimulacaoDeCredito.src.Infra.BaseSimulacao.Models;

namespace SimulacaoDeCredito.Domain.Repositories
{
    public interface ISimulacaoRepository
    {
        public Task<Simulacao?> ObterSimulacaoPorId(int idSimulacao);
        public Task<int> CriarSimulacao(Simulacao simulacao);
    }
}
