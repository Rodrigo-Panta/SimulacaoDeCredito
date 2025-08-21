using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimulacaoDeCredito.Application.Commands.CreateSimulacao;
using SimulacaoDeCredito.Application.Queries.GetSimulacoesPaginadas;
using SimulacaoDeCredito.Application.Queries.GetSimulacoesResumoDiario;

namespace SimulacaoDeCredito.Controllers;

[ApiController]
[Route("api/simulacoes")]
public class SimulacaoController : ControllerBase
{

    private readonly IMediator _mediator;

    private readonly ILogger<SimulacaoController> _logger;

    public SimulacaoController(ILogger<SimulacaoController> logger
    , IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateSimulacaoCommand command)
    {
        var result = await _mediator.Send(command);
        return Created("", result);
    }

    [HttpGet]
    public async Task<IActionResult> GetSimulacoes([FromQuery] GetSimulacoesQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("resumo-diario")]
    public async Task<IActionResult> GetSimulacoesPorDia([FromQuery] GetSimulacoesResumoDiarioQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

}

