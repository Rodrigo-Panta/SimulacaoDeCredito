using AutoMapper;
using MediatR;
using SimulacaoDeCredito.Application.DTOs.Response;
using SimulacaoDeCredito.Domain.EventPublishers;
using SimulacaoDeCredito.Domain.Factories;
using SimulacaoDeCredito.Domain.Repositories;

namespace SimulacaoDeCredito.Application.Commands;

public class CreateSimulacaoHandler : IRequestHandler<CreateSimulacaoCommand, CreateSimulacaoResponseDto>
{
    IProdutoRepository _produtoRepository;
    ISimulacaoRepository _simulacaoRepository;
    IMapper _mapper;
    IEventPublisher _eventPublisher;
    public CreateSimulacaoHandler(IProdutoRepository produtoRepository, ISimulacaoRepository simulacaoRepository, IMapper mapper, IEventPublisher eventPublisher)
    {
        _produtoRepository = produtoRepository;
        _simulacaoRepository = simulacaoRepository;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
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
        var simulacaoId = await _simulacaoRepository.CriarSimulacao(simulacao);
        simulacao.IdSimulacao = simulacaoId;

        await _eventPublisher.PublishAsync(simulacao);

        return _mapper.Map<CreateSimulacaoResponseDto>(simulacao);
    }
}
