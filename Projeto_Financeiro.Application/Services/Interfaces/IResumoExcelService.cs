using Projeto_Financeiro.Domain.ReadModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Financeiro.Application.Services.Interfaces
{
    public interface IResumoExcelService
    {
        void GerarExcelRelatorioResumo(List<ResumoFinanceiro> resumoFinanceiro, DateTime dataInicio, DateTime dataFim);
    }
}
