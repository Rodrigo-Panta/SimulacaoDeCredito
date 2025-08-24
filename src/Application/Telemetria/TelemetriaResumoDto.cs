namespace SimulacaoDeCredito.Application.Telemetria;

public class TelemetriaResumoDto
{
    public DateOnly Data { get; set; }
    public ICollection<EndpointResumoDto> Endpoints { get; set; } = [];
}

public class EndpointResumoDto
{
    public required string Endpoint { get; set; }
    public required long QtdRequisicoes { get; set; }
    public required long TempoMedioMs { get; set; }
    public required long TempoMinimoMs { get; set; }
    public required long TempoMaximoMs { get; set; }
    public required double PercentualSucesso { get; set; }
}