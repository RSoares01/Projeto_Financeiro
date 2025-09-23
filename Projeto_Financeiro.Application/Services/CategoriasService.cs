using Projeto_Financeiro.Application.DTOs;
using Projeto_Financeiro.Application.Services.Interfaces;
using Projeto_Financeiro.Domain.Entities;
using Projeto_Financeiro.Domain.Interfaces.IRepositories;
namespace Projeto_Financeiro.Application.Services
{
    public class CategoriaService : ICategoriasService
    {
        private readonly ICategoriasRepository _categoriasRepository;

        public CategoriaService(ICategoriasRepository categoriasRepository)
        {
            _categoriasRepository = categoriasRepository;
        }

        public async Task<CategoriasDTO?> GetCategoriasByIdAsync(int id)
        {
            var categorias = await _categoriasRepository.GetByIdAsync(id);
            if (categorias == null)
            {
                return null;
            }

            return new CategoriasDTO
            {
                Id = categorias.Id,
                Nome = categorias.Nome,
                Tipo = categorias.Tipo,
                Ativo = categorias.Ativo,
                DataCriacao = categorias.DataCriacao
            };
        }

        public async Task<IEnumerable<CategoriasDTO>> GetAllCategoriasAsync()
        {
            var categorias = await _categoriasRepository.GetAllAsync();

            return categorias.Select(c => new CategoriasDTO
            {
                Id = c.Id,
                Nome = c.Nome,
                Tipo = c.Tipo,
                Ativo = c.Ativo,
                DataCriacao = c.DataCriacao
            });
        }

        public async Task<CategoriasDTO> CreateCategoriasAsync(CategoriasDTO categoriasDTO)
        {
            var categorias = new Categorias(
                categoriasDTO.Nome,
                categoriasDTO.Tipo,
                categoriasDTO.Ativo,
                categoriasDTO.DataCriacao
            );
            await _categoriasRepository.CreateAsync(categorias);

            return new CategoriasDTO
            {
                Id = categorias.Id,
                Nome = categorias.Nome,
                Tipo = categorias.Tipo,
                Ativo = categorias.Ativo,
                DataCriacao = categorias.DataCriacao
            };
        }


        public async Task<CategoriasDTO> UpdateCategoriasAsync(CategoriasDTO categoriasDTO)
        {
            var categorias = await _categoriasRepository.GetByIdAsync(categoriasDTO.Id);

            if (categorias == null)
                throw new Exception("Categoria não encontrada.");

            categorias.Nome = categoriasDTO.Nome;
            categorias.Tipo = categoriasDTO.Tipo;
            categorias.Ativo = categoriasDTO.Ativo;
            categorias.DataCriacao = categoriasDTO.DataCriacao;

            await _categoriasRepository.UpdateAsync(categorias);

            return new CategoriasDTO
            {
                Id = categorias.Id,
                Nome = categorias.Nome,
                Tipo = categorias.Tipo,
                Ativo = categorias.Ativo,
                DataCriacao = categorias.DataCriacao
            };
        }

        public async Task RemoveCategoriasAsync(int id)
        {
            await _categoriasRepository.DeleteAsync(id);
        }
    }
}

