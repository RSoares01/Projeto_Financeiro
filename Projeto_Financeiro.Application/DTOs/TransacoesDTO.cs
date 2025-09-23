using Projeto_Financeiro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Financeiro.Application.DTOs
{
    public class TransacoesDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public string Observacoes { get; set; }
        public DateTime DataCriacao { get; set; }
        public int CategoriaId { get; set; }

    }
}
