using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Formulario_Cadastro_Cliente.Migrations
{
    /// <inheritdoc />
    public partial class AddCampoAtivoEmCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "clientes",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "clientes");
        }
    }
}
