using SimulacaoDeCredito.Application.Telemetria;

public class TelemetriaMiddleware
{
    private readonly RequestDelegate _next;

    public TelemetriaMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ITelemetriaService telemetria)
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
            var endpoint = context.Request.Path.Value?.Split("/").Last();
            if (string.IsNullOrEmpty(endpoint))
                endpoint = "index";
            await telemetria.RegistrarRequisicao(endpoint, (long)sw.Elapsed.TotalMilliseconds, sucesso);
        }
    }
}