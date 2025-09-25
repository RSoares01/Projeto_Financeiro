using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;
using Projeto_Financeiro.Application.Services;
using Projeto_Financeiro.Application.Services.Interfaces;
using Projeto_Financeiro.Domain.Interfaces.IRepositories;
using Projeto_Financeiro.Infrastructure.Context;
using Projeto_Financeiro.Infrastructure.Middleware;
using Projeto_Financeiro.Infrastructure.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

// Adiciona a configuração do JWT (lê do appsettings.json)
var jwtSettingsSection = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<JwtSettings>(jwtSettingsSection);
var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
var key = Encoding.UTF8.GetBytes(jwtSettings.Key);

// Add serviços ao container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Conexão com banco
builder.Services.AddDbContext<FinContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Injeção de dependência dos seus serviços e repositórios
builder.Services.AddScoped<ICategoriasService, CategoriaService>();
builder.Services.AddScoped<ICategoriasRepository, CategoriasRepository>();
builder.Services.AddScoped<ITransacoesService, TransacoesService>();
builder.Services.AddScoped<ITransacoesRepository, TransacoesRepository>();
builder.Services.AddScoped<IRelatorioRepository, RelatorioRepository>();
builder.Services.AddScoped<IObterResumoFinanceiroService, ObterResumoFinanceiroService>();
builder.Services.AddScoped<IObterRelatorioCategoriaService, ObterRelatorioCategoriaService>();
builder.Services.AddScoped<IResumoExcelService, ResumoExcelService>();
builder.Services.AddScoped<IRelatorioCategoriaExcelService, RelatorioCategoriaExcelService>();

// Configuração do JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false, // ajuste conforme necessário
        ValidateAudience = false, // ajuste conforme necessário
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateLifetime = true
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ADICIONE UseAuthentication ANTES de UseAuthorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


// Classe para carregar as configurações JWT
public class JwtSettings
{
    public string Key { get; set; }
    public int ExpireMinutes { get; set; }
}
