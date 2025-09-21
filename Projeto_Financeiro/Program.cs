using Microsoft.EntityFrameworkCore;
using Projeto_Financeiro.Application.Services;
using Projeto_Financeiro.Application.Services.Interfaces;
using Projeto_Financeiro.Infrastructure.Context;
using Projeto_Financeiro.Infrastructure.Repositories;
using Projeto_Financeiro.Domain.Interfaces.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Conexão com o banco
builder.Services.AddDbContext<FinContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Injeção de dependência
builder.Services.AddScoped<ICategoriasService, CategoriaService>();
builder.Services.AddScoped<ICategoriasRepository, CategoriasRepository>();

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
