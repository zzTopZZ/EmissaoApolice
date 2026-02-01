using Application;
using Application.Apolice.Ports;
using Application.Cliente.Ports;
using Application.Contratacao;
using Application.Contratacao.Ports;
using Application.Payment;
using Application.Proposta.Ports;
using Contratacao.Application.Manual;
using Data;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IClienteManager, ClienteManager>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

builder.Services.AddScoped<IPropostaManager, PropostaManager>();
builder.Services.AddScoped<IPropostaRepository, PropostaRepository>();

builder.Services.AddScoped<IApoliceManager, ApoliceManager>();
builder.Services.AddScoped<IApoliceRepository, ApoliceRepository>();

builder.Services.AddScoped<IContratacaoProcessorFactory, ContratacaoProcessorFactory>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EmissaoDbContext>(options =>
    options.UseSqlServer(connectionString));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllersWithViews()
                .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

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
