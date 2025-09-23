using Microsoft.AspNetCore.Mvc;
using Projeto_Financeiro.Application.DTOs;
using Projeto_Financeiro.Application.Services.Interfaces;

namespace Projeto_Financeiro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransacoesController : ControllerBase
    {
        private readonly ITransacoesService _service;

        public TransacoesController(ITransacoesService service)
        {
            _service = service;
        }

        /// <summary>
        /// Busca uma transação pelo Id.
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(TransacoesDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransacoesDTO>> GetById(int id, CancellationToken ct = default)
        {
            var dto = await _service.GetTransacoesByIdAsync(id);
            if (dto is null) return NotFound();
            return Ok(dto);
        }

        /// <summary>
        /// Lista todas as transações.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<object>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            if (page < 1 || pageSize < 1)
                return BadRequest(new { error = "page e pageSize devem ser maiores que zero." });

            var all = await _service.GetAllTransacoesAsync();

            var totalItems = all.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // evita pedir página além do fim
            if (totalItems > 0 && page > totalPages)
                page = totalPages;

            var items = all
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var response = new
            {
                currentPage = page,
                pageSize,
                totalItems,
                totalPages,
                items
            };

            return Ok(response);
        }

        /// <summary>
        /// Cria uma nova transação.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(TransacoesDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TransacoesDTO>> Create([FromBody] TransacoesDTO transacao, CancellationToken ct = default)
        {
            if (transacao is null) return BadRequest(new { error = "Corpo da requisição não pode ser nulo." });
            if (transacao.Valor <= 0)
                return BadRequest(new { error = "Valor deve ser maior que zero." });

            var created = await _service.CreateTransacoesAsync(transacao);

            // Gera Location: /api/transacoes/{id}
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Atualiza uma transação existente.
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(TransacoesDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransacoesDTO>> Update(int id, [FromBody] TransacoesDTO transacao)
        {
            if (transacao is null) return BadRequest(new { error = "Corpo da requisição não pode ser nulo." });
            if (id != transacao.Id) return BadRequest(new { error = "Id da rota difere do Id do corpo." });
            if (transacao.Valor <= 0)
                return BadRequest(new { error = "Valor deve ser maior que zero." });


            // (Opcional) validar existência antes, dependendo da sua service:
            var exists = await _service.GetTransacoesByIdAsync(id);
            if (exists is null) return NotFound();

            var updated = await _service.UpdateTransacoesAsync(transacao);
            return Ok(updated);
        }

        /// <summary>
        /// Remove uma transação pelo Id.
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _service.GetTransacoesByIdAsync(id);
            if (exists is null) return NotFound();

            await _service.RemoveTransacoesAsync(id);
            return NoContent();
        }
    }
}
