using Projeto_Financeiro.Domain.Entities;

namespace Projeto_Financeiro.Domain.Interfaces.IRepositories
{
    public interface ITransacoesRepository
    {
        Task<Transacoes?> GetByIdAsync(int id);
        Task<IEnumerable<Transacoes>> GetAllAsync();
        Task CreateAsync(Transacoes categoria);
        Task UpdateAsync(Transacoes categoria);
        Task DeleteAsync(int id);
    }
}
