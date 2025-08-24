using System.Net;
using System.Text.Json;
using SimulacaoDeCredito.Domain.Exceptions;

namespace SimulacaoDeCredito.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DomainException ex)
        {
            _logger.LogError(ex, "Exceção de domínio capturada");
            await HandleExceptionAsync(context, ex.Message, HttpStatusCode.BadRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exceção não tratada");
            await HandleExceptionAsync(context, "Ocorreu um erro inesperado", HttpStatusCode.InternalServerError);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, string message, HttpStatusCode statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var result = JsonSerializer.Serialize(new { erro = message });
        await context.Response.WriteAsync(result);
    }
}
