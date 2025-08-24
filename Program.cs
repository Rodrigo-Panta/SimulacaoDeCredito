using Microsoft.EntityFrameworkCore;
using SimulacaoDeCredito.Application.Profiles;
using SimulacaoDeCredito.Domain.EventPublishers;
using SimulacaoDeCredito.Domain.Repositories;
using SimulacaoDeCredito.Infra.Repositories;
using SimulacaoDeCredito.Infrastructure.BaseProduto.Persistence;
using SimulacaoDeCredito.Infra.EventPublishers;
using SimulacaoDeCredito.src.Infra.BaseSimulacao.Persistence;
using SimulacaoDeCredito.Infra.Telemetria;
using MongoDB.Driver;
using SimulacaoDeCredito.Application.Telemetria;
using SimulacaoDeCredito.Infra.Telemetria.InMemory;
using SimulacaoDeCredito.Infra.Telemetria.MongoDb;
using Microsoft.Extensions.Options;
using FluentValidation.AspNetCore;
using FluentValidation;
using SimulacaoDeCredito.Application.Commands.CreateSimulacao;
using Microsoft.AspNetCore.Mvc;
using SimulacaoDeCredito.API.Middlewares;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

// EF Core
builder.Services.AddDbContext<BaseProdutosDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BaseProdutoConnectionString")));
builder.Services.AddDbContext<BaseSimulacaoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BaseSimulacaoConnectionString")));

// Repositórios
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<ISimulacaoRepository, SimulacaoRepository>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// EventHub
var connectionString = Environment.GetEnvironmentVariable("EVENT_HUB_CONNECTION_STRING");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("A variável de ambiente 'EVENT_HUB_CONNECTION_STRING' não está definida.");
}
builder.Services.AddSingleton<IEventPublisher>(sp =>
    new EventHubPublisher(
        connectionString
    )
);

// MongoDB para Telemetria
builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection("MongoSettings"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddScoped<ITelemetriaService, MongoDBTelemetriaService>();

// FluentValidation
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddScoped<IValidator<CreateSimulacaoCommand>, CreateSimulacaoValidator>();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(x => x.Value!.Errors.Count > 0)
            .SelectMany(x => x.Value!.Errors.Select(e => e.ErrorMessage)).ToList();

        return new BadRequestObjectResult(new { erro = errors });
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<TelemetriaMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

