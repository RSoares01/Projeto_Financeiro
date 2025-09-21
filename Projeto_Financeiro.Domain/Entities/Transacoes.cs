
namespace Projeto_Financeiro.Domain.Entities
{
    public class Transacoes
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data {  get; set; }
        public string Observacoes { get; set; }
        public DateTime DataCriacao { get; set; }

        public int CategoriaId { get; set; }
        public Categorias categoria { get; set; } = null;
        
    }
}
