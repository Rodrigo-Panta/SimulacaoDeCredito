using MediatR;

namespace SimulacaoDeCredito.Application.Queries.GetSimulacoesPaginadas;

public record GetSimulacoesQuery(int pagina, int tamanho) : IRequest<ResultadoPaginadoDto<GetSimulacaoResponseDto>>;

