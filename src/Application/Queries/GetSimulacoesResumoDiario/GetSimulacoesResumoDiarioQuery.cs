using MediatR;

namespace SimulacaoDeCredito.Application.Queries.GetSimulacoesResumoDiario;

public record GetSimulacoesResumoDiarioQuery(DateTime Data) : IRequest<GetSimulacoesResumoDiarioResponseDto>;

