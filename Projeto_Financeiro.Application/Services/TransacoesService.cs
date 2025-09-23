using Projeto_Financeiro.Application.DTOs;
using Projeto_Financeiro.Application.Services.Interfaces;
using Projeto_Financeiro.Domain.Entities;
using Projeto_Financeiro.Domain.Interfaces.IRepositories;


namespace Projeto_Financeiro.Application.Services
{
    public class TransacoesService : ITransacoesService
    {
        private readonly ITransacoesRepository _transacoesRepository;

        public TransacoesService(ITransacoesRepository transacoesRepository)
        {
            _transacoesRepository = transacoesRepository;
        }

        public async Task<TransacoesDTO?> GetTransacoesByIdAsync(int id)
        {
            var transacoes = await _transacoesRepository.GetByIdAsync(id);
            if (transacoes == null)
            {
                return null;
            }

            return new TransacoesDTO
            {
                Id = transacoes.Id,
                Descricao = transacoes.Descricao,
                Valor = transacoes.Valor,
                Data = transacoes.Data,
                Observacoes = transacoes.Observacoes,
                DataCriacao = transacoes.DataCriacao,
                CategoriaId = transacoes.CategoriaId,
            };
        }

        public async Task<IEnumerable<TransacoesDTO>> GetAllTransacoesAsync()
        {
            var transacoes = await _transacoesRepository.GetAllAsync();

            return transacoes.Select(t => new TransacoesDTO
            {
                Id = t.Id,
                Descricao = t.Descricao,
                Valor = t.Valor,
                Data = t.Data,
                Observacoes = t.Observacoes,
                DataCriacao = t.DataCriacao,
                CategoriaId = t.CategoriaId,
            });
        }

        public async Task<TransacoesDTO> CreateTransacoesAsync(TransacoesDTO dto)
        {
            var transacao = new Transacoes(
                dto.Id,
                dto.Descricao,
                dto.Valor,
                dto.Data,
                dto.Observacoes,
                dto.DataCriacao,
                dto.CategoriaId
            );

            await _transacoesRepository.CreateAsync(transacao);

            return new TransacoesDTO
            {
                Id = transacao.Id,
                Descricao = transacao.Descricao,
                Valor = transacao.Valor,
                Data = transacao.Data,
                Observacoes = transacao.Observacoes,
                DataCriacao = transacao.DataCriacao,
                CategoriaId = transacao.CategoriaId
            };
        }




        public async Task<TransacoesDTO> UpdateTransacoesAsync(TransacoesDTO transacoesDTO)
        {
            var transacao = await _transacoesRepository.GetByIdAsync(transacoesDTO.Id);

            if (transacao == null)
                throw new Exception("Transacoes não encontrada.");

            transacao.Descricao = transacoesDTO.Descricao;
            transacao.Valor = transacoesDTO.Valor;
            transacao.Data = transacoesDTO.Data;
            transacao.Observacoes = transacoesDTO.Observacoes;
            transacao.DataCriacao = transacoesDTO.DataCriacao;
            transacao.CategoriaId = transacoesDTO.CategoriaId;

            await _transacoesRepository.UpdateAsync(transacao);

            return new TransacoesDTO
            {
                Id = transacao.Id,
                Descricao = transacao.Descricao,
                Valor = transacao.Valor,
                Data = transacao.Data,
                Observacoes = transacao.Observacoes,
                DataCriacao = transacao.DataCriacao,
                CategoriaId = transacao.CategoriaId
            };
        }

        public async Task RemoveTransacoesAsync(int id)
        {
            await _transacoesRepository.DeleteAsync(id);
        }
    }
}
