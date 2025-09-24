using Projeto_Financeiro.Domain.ReadModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Financeiro.Application.Services.Interfaces
{
    public interface IObterRelatorioCategoriaService
    {
        Task<List<RelatorioCategoria>> spRelatorioCategoriaAsync(DateTime dataInicio, DateTime dataFim);

    }
}
