using SistemaEstoque.Models;

public class MovimentacaoPerdaModel
{
    public int Id { get; set; }

    public int ProdutoId { get; set; }
    public ProdutoModel? Produto { get; set; }

    public int Quantidade { get; set; }

    public string? Motivo { get; set; }

    public DateTime DataMovimentacao { get; set; } = DateTime.Now;
}