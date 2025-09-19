using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Financeiro.Domain.Entity
{
    public class Categorias
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public char Tipo { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
