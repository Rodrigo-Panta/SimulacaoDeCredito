using System.Collections.Concurrent;

namespace SimulacaoDeCredito.Infra.Telemetria.InMemory;

public class TelemetriaStats
{
    public ConcurrentDictionary<string, EndpointStats> Endpoints { get; private set; }

    public TelemetriaStats()
    {
        Endpoints = new ConcurrentDictionary<string, EndpointStats>();
    }
}


public class EndpointStats
{
    public long TotalRequisicoes { get; set; }
    public long TempoTotalMs { get; set; }
    public long Sucesso { get; set; }
    public long SomaDuracao { get; set; }
    public long MinDuracao { get; set; } = long.MaxValue;
    public long MaxDuracao { get; set; } = long.MinValue;

    public EndpointStats()
    {
        TotalRequisicoes = 0;
        TempoTotalMs = 0;
        Sucesso = 0;
        SomaDuracao = 0;
    }
}