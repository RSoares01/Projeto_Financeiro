using OfficeOpenXml;
using OfficeOpenXml.Style;
using Projeto_Financeiro.Application.Services.Interfaces;
using Projeto_Financeiro.Domain.ReadModel;
using System.Drawing;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Projeto_Financeiro.Application.Services
{
    public class ResumoExcelService : IResumoExcelService
    {
        private readonly IObterResumoFinanceiroService _obterResumoFinanceiroService;
        private readonly string _savePath;

        public ResumoExcelService(IObterResumoFinanceiroService obterResumoFinanceiroService, IConfiguration configuration)
        {
            _obterResumoFinanceiroService = obterResumoFinanceiroService;
            _savePath = configuration["FileSettings:SavePath"]; // pegando do appsettings.json
        }

        public void GerarExcelRelatorioResumo(List<ResumoFinanceiro> resumoFinanceiro, DateTime dataInicio, DateTime dataFim)
        {
            using var package = new ExcelPackage();

            var worksheet = package.Workbook.Worksheets.Add("Resumo Financeiro");

            // Cabeçalhos
            worksheet.Cells[1, 1].Value = "Período";
            worksheet.Cells[1, 2].Value = "Saldo Total";
            worksheet.Cells[1, 3].Value = "Total Receitas";
            worksheet.Cells[1, 4].Value = "Total Despesas";

            using (var range = worksheet.Cells[1, 1, 1, 4])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
            }

            // Dados
            for (int i = 0; i < resumoFinanceiro.Count; i++)
            {
                var item = resumoFinanceiro[i];
                int row = i + 2;

                worksheet.Cells[row, 1].Value = $"{dataInicio:dd/MM/yyyy} - {dataFim:dd/MM/yyyy}";
                worksheet.Cells[row, 2].Value = item.SaldoTotal;
                worksheet.Cells[row, 3].Value = item.TotalReceitas;
                worksheet.Cells[row, 4].Value = item.TotalDespesas;

                worksheet.Cells[row, 2, row, 4].Style.Numberformat.Format = "R$ #,##0.00";
            }

            worksheet.Cells.AutoFitColumns();

            var segundoAtual = DateTime.Now.Second;
            var nomeArquivo = $"ResumoFinanceiro_{dataInicio:yyyyMMdd}_{dataFim:yyyyMMdd}{segundoAtual}.xlsx";
            var caminho = Path.Combine(_savePath, nomeArquivo);

            File.WriteAllBytes(caminho, package.GetAsByteArray());
        }
    }
}
