using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaDeControleDeEstoque.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cargo",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Cargo",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
