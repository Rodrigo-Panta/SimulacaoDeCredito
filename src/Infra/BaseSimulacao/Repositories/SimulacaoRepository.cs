using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimulacaoDeCredito.Domain.Entities;
using SimulacaoDeCredito.Domain.Repositories;
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

        public async Task<ICollection<Simulacao>> ObterSimulacoesPaginadas(int pagina, int tamanho)
        {
            return await _context.Simulacaos
                .Include(s => s.SimulacaoTabelas)
                .Skip((pagina - 1) * tamanho)
                .Take(tamanho)
                .Select(s => _mapper.Map<Simulacao>(s))
                .ToListAsync();
        }

        public async Task<int> CountSimulacoes()
        {
            return await _context.Simulacaos.CountAsync();
        }

        public async Task<ICollection<Simulacao>> ObterSimulacoesPorData(DateTime data)
        {
            var result = await _context.Simulacaos
                .Include(s => s.SimulacaoTabelas)
                .Where(s => s.DataCriacao.Date == data.Date)
                .Select(s => _mapper.Map<Simulacao>(s)).ToListAsync();
            return result;
        }


    }
}
