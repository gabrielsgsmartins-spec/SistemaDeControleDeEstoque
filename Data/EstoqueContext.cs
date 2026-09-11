using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaEstoque.Models;

namespace SistemaEstoque.Data
{
    public class EstoqueContext : IdentityDbContext<ApplicationUserModel>
    {
        public EstoqueContext(DbContextOptions<EstoqueContext> options) : base(options)
        {
        }

        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<CategoriaModel> Categorias { get; set; }
        public DbSet<MovimentacaoEntradaModel> MovimentacoesEntrada { get; set; }
        public DbSet<MovimentacaoVendaModel> MovimentacoesVenda { get; set; }
        public DbSet<MovimentacaoPerdaModel> MovimentacoesPerdas { get; set; }
        public DbSet<ApplicationUserModel> ApplicationUserModel { get; set; }
    }
}