using SistemaEstoque.Models;

public class MovimentacaoVendaModel
{
    public int Id { get; set; }

    public int ProdutoId { get; set; }
    public ProdutoModel? Produto { get; set; }

    public int Quantidade { get; set; }

    public DateTime DataMovimentacao { get; set; } = DateTime.Now;
}