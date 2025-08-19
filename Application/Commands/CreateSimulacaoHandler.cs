using MediatR;
using SimulacaoDeCredito.Application.DTOs.Response;

namespace SimulacaoDeCredito.Application.Commands;

public class CreateSimulacaoHandler : IRequestHandler<CreateSimulacaoCommand, CreateSimulacaoResponseDto>
{

    public CreateSimulacaoHandler()
    {
    }

    public async Task<CreateSimulacaoResponseDto> Handle(CreateSimulacaoCommand request, CancellationToken cancellationToken)
    {
        await Task.Delay(100, cancellationToken);
        return new CreateSimulacaoResponseDto()
        {
            codigoProduto = 1,
            descricaoProduto = "Produto 1",
            idSimulacao = 1,
            taxaJuros = 0.01m,
            resultadoSimulacao = new List<SimulacaoTabelaResponseDto>()
        };
    }
}
