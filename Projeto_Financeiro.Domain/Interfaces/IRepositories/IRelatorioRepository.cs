using Projeto_Financeiro.Domain.ReadModel;

namespace Projeto_Financeiro.Domain.Interfaces.IRepositories
{
    public interface IRelatorioRepository
    {
        Task<List<ResumoFinanceiro>> ObterResumoAsync(DateTime dataInicio, DateTime dataFim);

        Task<List<RelatorioCategoria>> ObterRelatorioByCategoriaAsync(DateTime dataInicio, DateTime dataFim);
    }

}
