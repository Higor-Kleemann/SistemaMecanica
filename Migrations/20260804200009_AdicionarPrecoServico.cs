using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaMecanica.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarPrecoServico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Preco",
                table: "Servicos",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Preco",
                table: "Servicos");
        }
    }
}
