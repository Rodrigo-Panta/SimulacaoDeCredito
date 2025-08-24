using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SimulacaoDeCredito.Application.Telemetria;

namespace SimulacaoDeCredito.Infra.Telemetria.MongoDb;

public class MongoDBTelemetriaService : ITelemetriaService
{
    private readonly IMongoCollection<TelemetriaMongoDBModel> _collection;

    public MongoDBTelemetriaService(IOptions<MongoSettings> settings, IMongoClient client)
    {
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _collection = database.GetCollection<TelemetriaMongoDBModel>(settings.Value.CollectionName);

    }

    public async Task RegistrarRequisicao(string endpoint, long duracaoMs, bool sucesso)
    {
        var data = DateTime.UtcNow.Date;

        var filter = Builders<TelemetriaMongoDBModel>.Filter.Eq(x => x.Data, data);

        var update = Builders<TelemetriaMongoDBModel>.Update
            .Inc($"Endpoints.{endpoint}.TotalRequisicoes", 1)
            .Inc($"Endpoints.{endpoint}.TempoTotalMs", duracaoMs)
            .Inc($"Endpoints.{endpoint}.Sucesso", sucesso ? 1 : 0)
            .Inc($"Endpoints.{endpoint}.SomaDuracao", duracaoMs)
            .Min($"Endpoints.{endpoint}.MinDuracao", duracaoMs)
            .Max($"Endpoints.{endpoint}.MaxDuracao", duracaoMs);

        await _collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
    }

    public async Task<IEnumerable<TelemetriaResumoDto>> GetResumo(DateTime? dataInicio = null, DateTime? dataFim = null)
    {
        var filter = Builders<TelemetriaMongoDBModel>.Filter.Empty;

        if (dataInicio.HasValue || dataFim.HasValue)
        {
            var builder = Builders<TelemetriaMongoDBModel>.Filter;
            filter = builder.Gte(x => x.Data, dataInicio ?? DateTime.MinValue) &
                     builder.Lte(x => x.Data, dataFim ?? DateTime.MaxValue);
        }

        var docs = await _collection.Find(filter).SortBy(d => d.Data).ToListAsync();

        if (!docs.Any())
        {
            return Enumerable.Empty<TelemetriaResumoDto>();
        }

        return docs.Select(d => new TelemetriaResumoDto
        {
            Data = DateOnly.FromDateTime(d.Data),
            Endpoints = d.Endpoints.Select(kv => new EndpointResumoDto
            {
                Endpoint = kv.Key,
                TempoMedioMs = kv.Value.TotalRequisicoes == 0 ? 0 :
                               (long)(kv.Value.TempoTotalMs / (decimal)kv.Value.TotalRequisicoes),
                PercentualSucesso = kv.Value.TotalRequisicoes == 0 ? 0 :
                                    Math.Round((double)kv.Value.Sucesso / kv.Value.TotalRequisicoes, 2),
                QtdRequisicoes = kv.Value.TotalRequisicoes,
                TempoMinimoMs = kv.Value.MinDuracao,
                TempoMaximoMs = kv.Value.MaxDuracao
            }).ToList()
        });
    }
}
