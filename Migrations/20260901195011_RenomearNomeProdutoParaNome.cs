using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaDeControleDeEstoque.Migrations
{
    /// <inheritdoc />
    public partial class RenomearNomeProdutoParaNome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NomeProduto",
                table: "Produtos",
                newName: "Nome");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Produtos",
                newName: "NomeProduto");
        }
    }
}
