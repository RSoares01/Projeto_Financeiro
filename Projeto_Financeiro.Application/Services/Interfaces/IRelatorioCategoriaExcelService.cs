using Projeto_Financeiro.Domain.ReadModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Financeiro.Application.Services.Interfaces
{
    public interface IRelatorioCategoriaExcelService
    {
        void GerarRelatorioByCategoriaExcel(List<RelatorioCategoria> relatorio, DateTime dataInicio, DateTime dataFim);
    }
}
