using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimulacaoDeCredito.Application.Commands;

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


}
