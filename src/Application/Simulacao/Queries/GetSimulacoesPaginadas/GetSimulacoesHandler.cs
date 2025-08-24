using AutoMapper;
using MediatR;
using SimulacaoDeCredito.Domain.Repositories;

namespace SimulacaoDeCredito.Application.Queries.GetSimulacoesPaginadas;

public class GetSimulacoesHandler : IRequestHandler<GetSimulacoesQuery, ResultadoPaginadoDto<GetSimulacaoResponseDto>>
{
    private readonly ISimulacaoRepository _simulacaoRepository;
    private readonly IMapper _mapper;

    public GetSimulacoesHandler(ISimulacaoRepository simulacaoRepository, IMapper mapper)
    {
        _mapper = mapper;
        _simulacaoRepository = simulacaoRepository;
    }

    public async Task<ResultadoPaginadoDto<GetSimulacaoResponseDto>> Handle(GetSimulacoesQuery request, CancellationToken cancellationToken)
    {
        var simulacoes = await _simulacaoRepository.ObterSimulacoesPaginadas(request.pagina, request.tamanho);
        var simulacoesDto = simulacoes.Select(s =>
        {
            return new GetSimulacaoResponseDto
            {
                IdSimulacao = s.IdSimulacao ?? 0,
                ValorDesejado = s.ValorDesejado,
                Prazo = s.Prazo,
                ValorTotalParcelas = s.ResultadoSimulacao.Select(t => new ValorTotalParcelasResponseDto
                {
                    Tipo = t.Tipo,
                    ValorTotalParcelas = t.ValorTotalParcelas
                }).ToList()
            };
        });

        var totalCount = await _simulacaoRepository.CountSimulacoes();
        return new ResultadoPaginadoDto<GetSimulacaoResponseDto>(
            request.pagina,
            simulacoes.Count,
            totalCount,
            simulacoesDto
        );
    }
}
