using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Financeiro.Domain.ReadModel
{
    public class ResumoFinanceiro
    {
        public decimal SaldoTotal { get; set; }
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
    }
}
