using MediatR;
using SimulacaoDeCredito.Application.DTOs.Response;
using SimulacaoDeCredito.Domain.Factories;
using SimulacaoDeCredito.Domain.Repositories;

namespace SimulacaoDeCredito.Application.Commands;

public class CreateSimulacaoHandler : IRequestHandler<CreateSimulacaoCommand, CreateSimulacaoResponseDto>
{
    IProdutoRepository _produtoRepository;
    public CreateSimulacaoHandler(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<CreateSimulacaoResponseDto> Handle(CreateSimulacaoCommand request, CancellationToken cancellationToken)
    {
        var valor = request.ValorDesejado;
        var prazo = request.Prazo;

        if (prazo <= 0)
            throw new Exception("Prazo deve ser maior que 0");
        if (valor <= 0)
            throw new Exception("Valor deve ser maior que 0");

        var produto = await _produtoRepository.ObterProdutoEnquadrado(valor, prazo);

        if (produto == null)
        {
            throw new Exception("Não foi encontrado um produto que se encaixa nos parâmetros");
        }

        var simulacao = SimulacaoFactory.Criar(prazo, valor, produto);
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
