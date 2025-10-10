using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Formulario_Cadastro_Cliente.Migrations
{
    /// <inheritdoc />
    public partial class AddCepCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CEP",
                table: "clientes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CEP",
                table: "clientes");
        }
    }
}
