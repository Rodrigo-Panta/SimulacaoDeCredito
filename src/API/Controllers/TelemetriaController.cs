using Microsoft.AspNetCore.Mvc;
using SimulacaoDeCredito.Infra.Telemetria;

namespace SimulacaoDeCredito.Controllers;

[ApiController]
[Route("api/telemetria")]
public class TelemetriaController : ControllerBase
{

    private readonly ILogger<TelemetriaController> _logger;
    private readonly ITelemetriaRepository _TelemetriaService;

    public TelemetriaController(ILogger<TelemetriaController> logger, ITelemetriaRepository TelemetriaService)
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

