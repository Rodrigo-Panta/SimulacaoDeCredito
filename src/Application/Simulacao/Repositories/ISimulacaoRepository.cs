using SimulacaoDeCredito.Application.Queries.GetSimulacoesResumoDiario;
using SimulacaoDeCredito.Domain.Entities;

namespace SimulacaoDeCredito.Domain.Repositories
{
    public interface ISimulacaoRepository
    {
        public Task<int> CriarSimulacao(Simulacao simulacao);

        public Task<ICollection<Simulacao>> ObterSimulacoesPaginadas(int pagina, int tamanho);

        public Task<ICollection<Simulacao>> ObterSimulacoesPorData(DateTime data);
        public Task<int> CountSimulacoes();
    }
}
