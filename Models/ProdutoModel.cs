using System.ComponentModel.DataAnnotations;

namespace SistemaEstoque.Models
{
    public class ProdutoModel
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public string? Descricao { get; set; }

        [Required]
        public int QuantidadeEmEstoque { get; set; }

        public int QuantidadeMinima { get; set; } = 5;

        [Required]
        public decimal PrecoUnitario { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        // Chave estrangeira
        public int CategoriaId { get; set; }
        public CategoriaModel? Categoria { get; set; }
    }
}