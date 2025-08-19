using Microsoft.EntityFrameworkCore;
using SimulacaoDeCredito.Domain.Repositories;
using SimulacaoDeCredito.Infra.Repositories;
using SimulacaoDeCredito.Infrastructure.BaseProduto.Persistence;

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

// Repositórios
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
