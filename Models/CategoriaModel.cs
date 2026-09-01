using SistemaEstoque.Models;
using System.ComponentModel.DataAnnotations;

public class CategoriaModel
{
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; }

    public List<ProdutoModel>? Produtos { get; set; }
}
