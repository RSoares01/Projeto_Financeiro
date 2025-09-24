using Projeto_Financeiro.Application.Services.Interfaces;
using Projeto_Financeiro.Domain.Interfaces.IRepositories;
using Projeto_Financeiro.Domain.ReadModel;

namespace Projeto_Financeiro.Application.Services
{
    public class ObterResumoFinanceiroService : IObterResumoFinanceiroService
    {
        private readonly IRelatorioRepository _repository;

        public ObterResumoFinanceiroService(IRelatorioRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ResumoFinanceiro>> spResumoAsync(DateTime dataInicio, DateTime dataFim)
        {
            return await _repository.ObterResumoAsync(dataInicio, dataFim);
        }
    }

}
