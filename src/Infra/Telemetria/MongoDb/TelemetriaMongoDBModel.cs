using System.Collections.Concurrent;
using MongoDB.Bson.Serialization.Attributes;

namespace SimulacaoDeCredito.Infra.Telemetria.MongoDb;

public class TelemetriaMongoDBModel
{
    [BsonId]
    public DateTime Data { get; set; }

    public ConcurrentDictionary<string, EndpointStats> Endpoints { get; set; } = new();
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