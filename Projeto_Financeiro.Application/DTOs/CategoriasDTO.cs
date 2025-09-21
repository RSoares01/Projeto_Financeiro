
namespace Projeto_Financeiro.Application.DTOs
{
    public class CategoriasDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public char Tipo { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
