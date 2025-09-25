using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
using OfficeOpenXml;
=======
using Projeto_Financeiro.Infrastructure.Middleware;
>>>>>>> TratativaErros
using Projeto_Financeiro.Application.Services;
using Projeto_Financeiro.Application.Services.Interfaces;
using Projeto_Financeiro.Domain.Interfaces.IRepositories;
using Projeto_Financeiro.Infrastructure.Context;
using Projeto_Financeiro.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


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
builder.Services.AddScoped<ITransacoesService, TransacoesService>();
builder.Services.AddScoped<ITransacoesRepository, TransacoesRepository>();
builder.Services.AddScoped<IRelatorioRepository, RelatorioRepository>();
builder.Services.AddScoped<IObterResumoFinanceiroService, ObterResumoFinanceiroService>();
builder.Services.AddScoped<IObterRelatorioCategoriaService,  ObterRelatorioCategoriaService>();
builder.Services.AddScoped<IResumoExcelService, ResumoExcelService>();
builder.Services.AddScoped<IRelatorioCategoriaExcelService, RelatorioCategoriaExcelService>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();


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
