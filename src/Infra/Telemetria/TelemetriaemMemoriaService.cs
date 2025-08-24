using System.Collections.Concurrent;

namespace SimulacaoDeCredito.Infra.Telemetria;

public class InMemoryTelemetriaService : ITelemetriaRepository
{
    private readonly ConcurrentDictionary<DateTime, TelemetriaStats> _stats = new();

    public Task RegistrarRequisicao(string endpoint, long duracaoMs, bool sucesso)
    {
        var data = DateTime.Now.Date;
        var stats = _stats.TryGetValue(data, out var existingStats) ? existingStats : new TelemetriaStats();

        var endpointStats = stats.EndpointStats.TryGetValue(endpoint, out var existingEp) ? existingEp : new EndpointStats();
        endpointStats.TotalRequisicoes++;
        endpointStats.TempoTotalMs += (long)duracaoMs;
        endpointStats.Sucesso += sucesso ? 1 : 0;
        endpointStats.SomaDuracao += duracaoMs;
        endpointStats.MinDuracao = Math.Min(endpointStats.MinDuracao, duracaoMs);
        endpointStats.MaxDuracao = Math.Max(endpointStats.MaxDuracao, duracaoMs);
        stats.EndpointStats[endpoint] = endpointStats;
        _stats[data] = stats;

        return Task.CompletedTask;
    }

    public Task<IEnumerable<TelemetriaResumoDto>> GetResumo(DateTime? dataInicio = null, DateTime? dataFim = null)
    {
        var datas = _stats.Keys
            .Where(d => (!dataInicio.HasValue || d >= dataInicio.Value) && (!dataFim.HasValue || d <= dataFim.Value))
            .OrderBy(d => d)
            .ToList();

        var resumo = new List<TelemetriaResumoDto>();

        return Task.FromResult(datas.Select(data => new TelemetriaResumoDto
        {
            Data = DateOnly.FromDateTime(data),
            Endpoints = _stats[data].EndpointStats.Select(s => new EndpointResumoDto
            {
                Endpoint = s.Key,
                TempoMedioMs = (long)(s.Value.TempoTotalMs / (decimal)s.Value.TotalRequisicoes),
                PercentualSucesso = Math.Round((double)s.Value.Sucesso / s.Value.TotalRequisicoes, 2),
                QtdRequisicoes = s.Value.TotalRequisicoes,
                TempoMinimoMs = s.Value.MinDuracao,
                TempoMaximoMs = s.Value.MaxDuracao

            }).ToList()
        }));

    }

}

