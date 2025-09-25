using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Projeto_Financeiro.Application.Services.Interfaces;
using Projeto_Financeiro.Domain.ReadModel;

public class RelatorioCategoriaExcelService : IRelatorioCategoriaExcelService
{
    private readonly string _savePath;

    public RelatorioCategoriaExcelService(IConfiguration configuration)
    {
        _savePath = configuration.GetSection("FileSettings:SavePath").Value;

    }

    public void GerarRelatorioByCategoriaExcel(List<RelatorioCategoria> relatorio, DateTime dataInicio, DateTime dataFim)
    {
        using var package = new ExcelPackage();

        var worksheet = package.Workbook.Worksheets.Add("Relatório por Categoria");

        // Cabeçalhos
        worksheet.Cells[1, 1].Value = "Categoria ID";
        worksheet.Cells[1, 2].Value = "Nome";
        worksheet.Cells[1, 3].Value = "Tipo";
        worksheet.Cells[1, 4].Value = "Qtde Movimentos";
        worksheet.Cells[1, 5].Value = "Soma dos Valores";

        using (var range = worksheet.Cells[1, 1, 1, 5])
        {
            range.Style.Font.Bold = true;
            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
            range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
        }

        for (int i = 0; i < relatorio.Count; i++)
        {
            var item = relatorio[i];
            int row = i + 2;

            worksheet.Cells[row, 1].Value = item.CategoriaId;
            worksheet.Cells[row, 2].Value = item.CategoriaNome;
            worksheet.Cells[row, 3].Value = item.CategoriaTipo;
            worksheet.Cells[row, 4].Value = item.QuantidadeMovimentos;
            worksheet.Cells[row, 5].Value = item.SomaValores;
            worksheet.Cells[row, 5].Style.Numberformat.Format = "R$ #,##0.00";
        }

        worksheet.Cells.AutoFitColumns();

        var segundoAtual = DateTime.Now.Second;
        var nomeArquivo = $"RelatorioPorCategoria_{dataInicio:yyyyMMdd}_{dataFim:yyyyMMdd}{segundoAtual}.xlsx";
        var caminho = Path.Combine(_savePath, nomeArquivo);

        File.WriteAllBytes(caminho, package.GetAsByteArray());
    }
}
