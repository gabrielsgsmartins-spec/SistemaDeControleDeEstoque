using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaDeControleDeEstoque.Migrations
{
    /// <inheritdoc />
    public partial class AlterarCategoriaProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimentacoes_Produtos_ProdutoId",
                table: "Movimentacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Categorias_CategoriaId",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_CategoriaId",
                table: "Produtos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movimentacoes",
                table: "Movimentacoes");

            migrationBuilder.RenameTable(
                name: "Movimentacoes",
                newName: "MovimentacaoEstoqueModel");

            migrationBuilder.RenameIndex(
                name: "IX_Movimentacoes_ProdutoId",
                table: "MovimentacaoEstoqueModel",
                newName: "IX_MovimentacaoEstoqueModel_ProdutoId");

            migrationBuilder.AddColumn<string>(
                name: "Categoria",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaModelId",
                table: "Produtos",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovimentacaoEstoqueModel",
                table: "MovimentacaoEstoqueModel",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaModelId",
                table: "Produtos",
                column: "CategoriaModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentacaoEstoqueModel_Produtos_ProdutoId",
                table: "MovimentacaoEstoqueModel",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Categorias_CategoriaModelId",
                table: "Produtos",
                column: "CategoriaModelId",
                principalTable: "Categorias",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovimentacaoEstoqueModel_Produtos_ProdutoId",
                table: "MovimentacaoEstoqueModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Categorias_CategoriaModelId",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_CategoriaModelId",
                table: "Produtos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovimentacaoEstoqueModel",
                table: "MovimentacaoEstoqueModel");

            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "CategoriaModelId",
                table: "Produtos");

            migrationBuilder.RenameTable(
                name: "MovimentacaoEstoqueModel",
                newName: "Movimentacoes");

            migrationBuilder.RenameIndex(
                name: "IX_MovimentacaoEstoqueModel_ProdutoId",
                table: "Movimentacoes",
                newName: "IX_Movimentacoes_ProdutoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movimentacoes",
                table: "Movimentacoes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaId",
                table: "Produtos",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimentacoes_Produtos_ProdutoId",
                table: "Movimentacoes",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Categorias_CategoriaId",
                table: "Produtos",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
