
namespace Projeto_Financeiro.Domain.Entities
{
    public class Categorias
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public char Tipo { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }

        public Categorias(string nome, char tipo, bool ativo, DateTime dataCriacao)
        {
            Nome = nome;
            Tipo = tipo;
            Ativo = ativo;
            DataCriacao = dataCriacao;
        }

        public Categorias() { }
    }
}
