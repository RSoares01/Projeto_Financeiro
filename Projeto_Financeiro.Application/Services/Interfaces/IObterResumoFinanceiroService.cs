using Projeto_Financeiro.Domain.ReadModel;

namespace Projeto_Financeiro.Application.Services.Interfaces
{
    public interface IObterResumoFinanceiroService
    {
        Task<List<ResumoFinanceiro>> spResumoAsync(DateTime dataInicio, DateTime dataFim);
    }
}
