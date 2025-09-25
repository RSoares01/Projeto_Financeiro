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

            if (dto is null)
                throw new KeyNotFoundException("Transação não encontrada.");

            return Ok(dto);
        }

        /// <summary>
        /// Lista todas as transações.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<object>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            if (page < 1 || pageSize < 1)
                throw new ArgumentException("page e pageSize devem ser maiores que zero.");

            var all = await _service.GetAllTransacoesAsync();

            var totalItems = all.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

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
            if (transacao is null)
                throw new ArgumentNullException(nameof(transacao), "Corpo da requisição não pode ser nulo.");

            if (transacao.Valor <= 0)
                throw new ArgumentException("Valor deve ser maior que zero.");

            var created = await _service.CreateTransacoesAsync(transacao);

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
            if (transacao is null)
                throw new ArgumentNullException(nameof(transacao), "Corpo da requisição não pode ser nulo.");

            if (id != transacao.Id)
                throw new ArgumentException("Id da rota diferente do Id do corpo.");

            if (transacao.Valor <= 0)
                throw new ArgumentException("Valor deve ser maior que zero.");

            var exists = await _service.GetTransacoesByIdAsync(id);
            if (exists is null)
                throw new KeyNotFoundException("Transação não encontrada.");

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
            if (exists is null)
                throw new KeyNotFoundException("Transação não encontrada.");

            await _service.RemoveTransacoesAsync(id);
            return NoContent();
        }
    }
}
