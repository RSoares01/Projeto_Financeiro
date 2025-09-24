using Projeto_Financeiro.Domain.ReadModel;

namespace Projeto_Financeiro.Domain.Interfaces.IRepositories
{
    public interface IRelatorioRepository
    {
        Task<ResumoFinanceiro> ObterResumoAsync(DateTime dataInicio, DateTime dataFim);

        //Task<List<ResumoFinanceiro>> ObterResumoPorCategoriaAsync(DateTime dataInicio, DateTime dataFim);
    }

}
