using SimulacaoDeCredito.Infra.Telemetria;

public class TelemetriaMiddleware
{
    private readonly RequestDelegate _next;

    public TelemetriaMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ITelemetriaRepository telemetria)
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();
        bool sucesso = false;

        try
        {
            await _next(context);
            sucesso = context.Response.StatusCode < 400;
        }
        finally
        {
            sw.Stop();
            var endpoint = context.Request.Path.Value?.Split("/").LastOrDefault() ?? "desconhecido";
            await telemetria.RegistrarRequisicao(endpoint, (long)sw.Elapsed.TotalMilliseconds, sucesso);
        }
    }
}