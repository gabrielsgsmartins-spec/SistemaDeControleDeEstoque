using System.ComponentModel.DataAnnotations;
using SistemaEstoque.Enums;

namespace SistemaEstoque.Models
{
    public class EntradaEstoqueModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        public ProdutoModel? Produto { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        public string Tipo { get; set; } = string.Empty;

        [StringLength(50)]
        public string? NotaFiscal { get; set; }

        public DateTime DataMovimentacao { get; set; } = DateTime.Now;

        [Required]
        [StringLength(100)]
        public string Responsavel { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Observacao { get; set; }
    }
}