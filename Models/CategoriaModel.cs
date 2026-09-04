using System.ComponentModel.DataAnnotations;

namespace SistemaEstoque.Models
{
    public class CategoriaModel
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public List<ProdutoModel> Produtos { get; set; }
            = new List<ProdutoModel>();
    }
}