using Microsoft.EntityFrameworkCore;
using SistemaEstoque.Models;
using SistemaEstoque.Data;
namespace SistemaEstoque.Data
{
    public class EstoqueContext : DbContext
    {
        public EstoqueContext(DbContextOptions<EstoqueContext> options) : base(options)
        {
        }

        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<CategoriaModel> Categorias { get; set; }
        public DbSet<MovimentacaoEstoqueModel> Movimentacoes { get; set; }
    }
}