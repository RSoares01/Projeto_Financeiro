using Projeto_Financeiro.Application.Services.Interfaces;
using Projeto_Financeiro.Domain.Interfaces.IRepositories;
using Projeto_Financeiro.Domain.ReadModel;

namespace Projeto_Financeiro.Application.Services
{
    public class ObterRelatorioCategoriaService : IObterRelatorioCategoriaService
    {
        private readonly IRelatorioRepository _repository;

        public ObterRelatorioCategoriaService(IRelatorioRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<RelatorioCategoria>> spRelatorioCategoriaAsync(DateTime dataInicio, DateTime dataFim)
        {
            return await _repository.ObterRelatorioByCategoriaAsync(dataInicio, dataFim);
        }
    }
}
