using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaMecanica.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoVeiculo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "KM",
                table: "Veiculos",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KM",
                table: "Veiculos");
        }
    }
}
