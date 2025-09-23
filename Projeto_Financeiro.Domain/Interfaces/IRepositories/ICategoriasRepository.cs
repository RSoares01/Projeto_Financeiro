using Projeto_Financeiro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Financeiro.Domain.Interfaces.IRepositories
{
    public interface ICategoriasRepository
    {
        Task<Categorias?> GetByIdAsync(int id);
        Task<IEnumerable<Categorias>> GetAllAsync();
        Task CreateAsync(Categorias categoria);
        Task UpdateAsync(Categorias categoria);
        Task DeleteAsync (int id);
    }
}
