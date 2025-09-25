using Microsoft.AspNetCore.Mvc;
using Projeto_Financeiro.Application.Services.Interfaces;

namespace Projeto_Financeiro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatoriosController : ControllerBase
    {
        private readonly IObterResumoFinanceiroService _obterResumoFinanceiroService;
        private readonly IObterRelatorioCategoriaService _obterRelatorioCategoriaService;
        private readonly IResumoExcelService _resumoExcelService;
        private readonly IRelatorioCategoriaExcelService _relatorioCategoriaExcelService;

        public RelatoriosController(
            IObterResumoFinanceiroService obterResumoFinanceiroService,
            IObterRelatorioCategoriaService obterRelatorioCategoriaService,
            IResumoExcelService resumoExcelService,
            IRelatorioCategoriaExcelService relatorioCategoriaExcelService)
        {
            _obterResumoFinanceiroService = obterResumoFinanceiroService;
            _obterRelatorioCategoriaService = obterRelatorioCategoriaService;
            _resumoExcelService = resumoExcelService;
            _relatorioCategoriaExcelService = relatorioCategoriaExcelService;
        }

        /// <summary>
        /// Obtém o resumo financeiro entre duas datas e gera relatório em Excel.
        /// </summary>
        [HttpGet("resumo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObterResumo([FromQuery] DateTime dataInicio, [FromQuery] DateTime dataFim)
        {
            if (dataInicio > dataFim)
                throw new ArgumentException("A data de início não pode ser maior que a data de fim.");

            var resumo = await _obterResumoFinanceiroService.spResumoAsync(dataInicio, dataFim);

            if (resumo == null || !resumo.Any())
                throw new KeyNotFoundException("Nenhum resumo financeiro encontrado para o período especificado.");

            _resumoExcelService.GerarExcelRelatorioResumo(resumo, dataInicio, dataFim);

            return Ok(resumo);
        }

        /// <summary>
        /// Obtém o relatório financeiro por categoria entre duas datas e gera relatório em Excel.
        /// </summary>
        [HttpGet("por-categoria")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObterResumoPorCategoria([FromQuery] DateTime dataInicio, [FromQuery] DateTime dataFim)
        {
            if (dataInicio > dataFim)
                throw new ArgumentException("A data de início não pode ser maior que a data de fim.");

            var resumo = await _obterRelatorioCategoriaService.spRelatorioCategoriaAsync(dataInicio, dataFim);

            if (resumo == null || !resumo.Any())
                throw new KeyNotFoundException("Nenhum resumo por categoria encontrado para o período especificado.");

            _relatorioCategoriaExcelService.GerarRelatorioByCategoriaExcel(resumo, dataInicio, dataFim);

            return Ok(resumo);
        }
    }
}
