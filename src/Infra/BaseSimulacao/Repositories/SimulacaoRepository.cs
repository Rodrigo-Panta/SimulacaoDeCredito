using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimulacaoDeCredito.Domain.Entities;
using SimulacaoDeCredito.Domain.Repositories;
using SimulacaoDeCredito.Infrastructure.BaseProduto.Persistence;
using SimulacaoDeCredito.src.Infra.BaseSimulacao.Models;
using SimulacaoDeCredito.src.Infra.BaseSimulacao.Persistence;

namespace SimulacaoDeCredito.Infra.Repositories
{

    public class SimulacaoRepository : ISimulacaoRepository
    {
        private readonly BaseSimulacaoDbContext _context;
        private readonly IMapper _mapper;

        public SimulacaoRepository(BaseSimulacaoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CriarSimulacao(Simulacao simulacao)
        {
            var simulacaoModel = _mapper.Map<SimulacaoModel>(simulacao);
            _context.Simulacaos.Add(simulacaoModel);
            await _context.SaveChangesAsync();
            return simulacaoModel.IdSimulacao ?? throw new InvalidOperationException("IdSimulacao cannot be null after saving.");
        }

        public Task<Simulacao?> ObterSimulacaoPorId(int idSimulacao)
        {
            throw new NotImplementedException();
        }
    }
}
