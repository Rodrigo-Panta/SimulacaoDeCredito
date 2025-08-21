using MediatR;

namespace SimulacaoDeCredito.Application.Commands.CreateSimulacao;

public record CreateSimulacaoCommand(decimal ValorDesejado, int Prazo) : IRequest<CreateSimulacaoResponseDto>;


