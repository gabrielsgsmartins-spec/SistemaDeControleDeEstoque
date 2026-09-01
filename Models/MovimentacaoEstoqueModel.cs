using System.ComponentModel.DataAnnotations;
using SistemaEstoque.Enums;

namespace SistemaEstoque.Models
{
    public class MovimentacaoEstoqueModel
    {
        public int Id { get; set; }

        [Required]
        public int ProdutoId { get; set; }
        public ProdutoModel? Produto { get; set; }

        [Required(ErrorMessage = "Informe o tipo de movimentação")]
        public TipoMovimentacaoEnum Tipo { get; set; }

        [Required(ErrorMessage = "Informe a quantidade")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero")]
        public int Quantidade { get; set; }

        public string? Motivo { get; set; }

        public DateTime DataMovimentacao { get; set; } = DateTime.Now;
    }
}