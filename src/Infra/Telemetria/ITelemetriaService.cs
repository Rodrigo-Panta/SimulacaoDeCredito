namespace SimulacaoDeCredito.Infra.Telemetria;

public interface ITelemetriaService
{
    public Task RegistrarRequisicao(string endpoint, long duracaoMs, bool sucesso);

    public Task<IEnumerable<TelemetriaResumoDto>> GetResumo(DateTime? dataInicio = null, DateTime? dataFim = null);
}
