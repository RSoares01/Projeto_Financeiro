using Projeto_Financeiro.Application.DTOs;

namespace Projeto_Financeiro.Application.Services.Interfaces
{
    public interface ITransacoesService
    {
        Task<TransacoesDTO?> GetTransacoesByIdAsync(int id);
        Task<IEnumerable<TransacoesDTO>> GetAllTransacoesAsync();
        Task<TransacoesDTO> CreateTransacoesAsync(TransacoesDTO transacoes);
        Task<TransacoesDTO> UpdateTransacoesAsync(TransacoesDTO transacoes);
        Task RemoveTransacoesAsync(int id);
    }
}
