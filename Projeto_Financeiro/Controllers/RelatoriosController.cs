using Microsoft.AspNetCore.Mvc;
using Projeto_Financeiro.Application.Services.Interfaces;
using System.Reflection.Metadata;

namespace Projeto_Financeiro.Controllers
{
    public class RelatoriosController : ControllerBase
    {
        private readonly IObterResumoFinanceiroService _obterResumoFinanceiroService;
        private readonly IObterRelatorioCategoriaService _obterRelatorioCategoriaService; 
        private readonly IResumoExcelService _resumoExcelService;
        private readonly IRelatorioCategoriaExcelService _relatorioCategoriaExcelService;

        public RelatoriosController(IObterResumoFinanceiroService obterResumoFinanceiroService, IObterRelatorioCategoriaService obterRelatorioCategoriaService, IResumoExcelService resumoExcelService, IRelatorioCategoriaExcelService relatorioCategoriaExcelService)
        {
            _obterResumoFinanceiroService = obterResumoFinanceiroService;
            _obterRelatorioCategoriaService = obterRelatorioCategoriaService;
            _resumoExcelService = resumoExcelService;
            _relatorioCategoriaExcelService = relatorioCategoriaExcelService;
        }

        [HttpGet("resumo")]
        public async Task<IActionResult> ObterResumo([FromQuery] DateTime dataInicio, [FromQuery] DateTime dataFim)
        {
            var resumo = await _obterResumoFinanceiroService.spResumoAsync(dataInicio, dataFim);
            if (resumo == null)
                return NotFound();

            _resumoExcelService.GerarExcelRelatorioResumo(resumo, dataInicio, dataFim);

            return Ok(resumo);
        }


        [HttpGet("por-categoria")]
        public async Task<IActionResult> ObterResumoPorCategoria([FromQuery] DateTime dataInicio, [FromQuery] DateTime dataFim)
        {
            var resumo = await _obterRelatorioCategoriaService.spRelatorioCategoriaAsync(dataInicio, dataFim);
            if (resumo == null)
                return NotFound();

            _relatorioCategoriaExcelService.GerarRelatorioByCategoriaExcel(resumo, dataInicio, dataFim);

            return Ok(resumo);
        }
    }
}
