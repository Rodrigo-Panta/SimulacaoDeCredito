using Microsoft.AspNetCore.Mvc;
using SimulacaoDeCredito.Application.Telemetria;
using SimulacaoDeCredito.Infra.Telemetria;

namespace SimulacaoDeCredito.Controllers;

[ApiController]
[Route("api/telemetria")]
public class TelemetriaController : ControllerBase
{

    private readonly ILogger<TelemetriaController> _logger;
    private readonly ITelemetriaService _TelemetriaService;

    public TelemetriaController(ILogger<TelemetriaController> logger, ITelemetriaService TelemetriaService)
    {
        _TelemetriaService = TelemetriaService;
        _logger = logger;
    }



    [HttpGet]
    public async Task<IActionResult> GetResumoPorData([FromQuery] DateTime? dataInicio = null, [FromQuery] DateTime? dataFim = null)
    {
        var result = await _TelemetriaService.GetResumo(dataInicio, dataFim);
        return Ok(result);
    }

}

