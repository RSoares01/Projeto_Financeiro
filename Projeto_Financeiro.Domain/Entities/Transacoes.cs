
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
        public Categorias Categoria { get; set; }

        public Transacoes(int id, string descricao, decimal valor, DateTime data, string observacoes, DateTime dataCriacao, int categoriaId)
        {
            Id = id;
            Descricao = descricao;
            Valor = valor;
            Data = data;
            Observacoes = observacoes;
            DataCriacao = dataCriacao;
            CategoriaId = categoriaId;
        }

        public Transacoes() { }
    }
}
