using AutoMapper;
using MediatR;
using SimulacaoDeCredito.Domain.Entities;
using SimulacaoDeCredito.Domain.Repositories;

namespace SimulacaoDeCredito.Application.Queries.GetSimulacoesResumoDiario;

public class GetSimulacoesResumoDiarioHandler : IRequestHandler<GetSimulacoesResumoDiarioQuery, GetSimulacoesResumoDiarioResponseDto>
{
    private readonly ISimulacaoRepository _simulacaoRepository;
    private readonly IMapper _mapper;

    public GetSimulacoesResumoDiarioHandler(ISimulacaoRepository simulacaoRepository, IMapper mapper)
    {
        _simulacaoRepository = simulacaoRepository;
        _mapper = mapper;
    }

    public async Task<GetSimulacoesResumoDiarioResponseDto> Handle(GetSimulacoesResumoDiarioQuery request, CancellationToken cancellationToken)
    {
        // TODO: Mudar essa lógica para uma query no banco se ficar muito lenta
        var simulacoes = await _simulacaoRepository.ObterSimulacoesPorData(request.Data);

        var resposta = new List<SimulacaoResumoDiarioDto>();

        Dictionary<int, List<Simulacao>> resumoPorProduto = new();
        foreach (var simulacao in simulacoes)
        {
            if (!resumoPorProduto.ContainsKey(simulacao.CodigoProduto))
            {
                resumoPorProduto[simulacao.CodigoProduto] = new List<Simulacao>();
            }
            resumoPorProduto[simulacao.CodigoProduto].Add(simulacao);
        }

        foreach (var entry in resumoPorProduto)
        {
            var responseDto = new SimulacaoResumoDiarioDto
            {
                CodigoProduto = entry.Key,
                DescricaoProduto = entry.Value.FirstOrDefault()?.DescricaoProduto,
                TaxaMediaJuro = entry.Value.Average(s => s.TaxaJuros),
                ValorMedioPrestacao = Math.Round(entry.Value.Average(s => s.ResultadoSimulacao.Sum(t => t.ValorTotalParcelas / s.Prazo)), 2, MidpointRounding.AwayFromZero),
                ValorTotalDesejado = Math.Round(entry.Value.Sum(s => s.ValorDesejado), 2, MidpointRounding.AwayFromZero),
                ValorTotalCredito = Math.Round(entry.Value.Average(s => s.ResultadoSimulacao.Sum(t => t.ValorTotalParcelas)), 2, MidpointRounding.AwayFromZero)
            };

            resposta.Add(responseDto);
        }

        return new GetSimulacoesResumoDiarioResponseDto
        {
            Data = DateOnly.FromDateTime(request.Data),
            Simulacoes = resposta
        };

    }
}
