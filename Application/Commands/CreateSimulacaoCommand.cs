using MediatR;
using SimulacaoDeCredito.Application.DTOs.Response;

namespace SimulacaoDeCredito.Application.Commands
{
    public record CreateSimulacaoCommand(decimal ValorDesejado, int Prazo) : IRequest<CreateSimulacaoResponseDto>;

}
