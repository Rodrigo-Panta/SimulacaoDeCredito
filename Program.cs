using Microsoft.EntityFrameworkCore;
using SimulacaoDeCredito.Application.Profiles;
using SimulacaoDeCredito.Domain.EventPublishers;
using SimulacaoDeCredito.Domain.Repositories;
using SimulacaoDeCredito.Infra.Repositories;
using SimulacaoDeCredito.Infrastructure.BaseProduto.Persistence;
using SimulacaoDeCredito.Infra.EventPublishers;
using SimulacaoDeCredito.src.Infra.BaseSimulacao.Persistence;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using SimulacaoDeCredito.Infra.Telemetria;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
var connectionString = "Endpoint=sb://localhost;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=MinhaSharedAccessKey;UseDevelopmentEmulator=true;EntityPath=simulacao";
builder.Services.AddSingleton<IEventPublisher>(sp =>
    new EventHubPublisher(
        connectionString
    )
);

builder.Services.AddSingleton<ITelemetriaService, InMemoryTelemetriaService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<TelemetriaMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

// await using var consumer = new EventHubConsumerClient(consumerGroup, connectionString);

// Console.WriteLine("Lendo eventos do Event Hub...");

// using CancellationTokenSource cancellationSource = new CancellationTokenSource();
// cancellationSource.CancelAfter(TimeSpan.FromSeconds(30)); // lê por 30 segundos

// await foreach (PartitionEvent partitionEvent in consumer.ReadEventsAsync(cancellationSource.Token))
// {
//     string data = Encoding.UTF8.GetString(partitionEvent.Data.Body.ToArray());
//     Console.WriteLine($"Evento recebido: {data}");
// }