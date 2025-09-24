using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Financeiro.Domain.ReadModel
{
    public class RelatorioCategoria
    {
        public int CategoriaId { get; set; }
        public string CategoriaNome { get; set; }
        public string CategoriaTipo { get; set; }
        public int QuantidadeMovimentos { get; set; }
        public decimal SomaValores { get; set; }
    }
}
