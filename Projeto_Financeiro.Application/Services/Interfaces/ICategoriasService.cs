using Projeto_Financeiro.Application.DTOs;
using Projeto_Financeiro.Domain.Entities;

namespace Projeto_Financeiro.Application.Services.Interfaces
{
    public interface ICategoriasService
    {
        Task<CategoriasDTO?> GetCategoriasByIdAsync(int id);
        Task<IEnumerable<CategoriasDTO>> GetAllCategoriasAsync();
        Task <CategoriasDTO> CreateCategoriasAsync(CategoriasDTO categoria);
        Task<CategoriasDTO> UpdateCategoriasAsync(CategoriasDTO categoria);
        Task RemoveCategoriasAsync(int id);
    }
}
