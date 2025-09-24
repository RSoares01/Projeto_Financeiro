using Microsoft.AspNetCore.Mvc;
using Projeto_Financeiro.Application.Services.Interfaces;
using System.Reflection.Metadata;

namespace Projeto_Financeiro.Controllers
{
    public class RelatorioController : ControllerBase
    {
        private readonly IObterResumoFinanceiroService _obterResumoFinanceiroService;

        public RelatorioController(IObterResumoFinanceiroService obterResumoFinanceiroService)
        {
            _obterResumoFinanceiroService = obterResumoFinanceiroService;
        }

        [HttpGet("resumo")]
        public async Task<IActionResult> ObterResumo([FromQuery] DateTime dataInicio, [FromQuery] DateTime dataFim)
        {
            var resumo = await _obterResumoFinanceiroService.spResumoAsync(dataInicio, dataFim);
            return Ok(resumo);
        }
    }
}
