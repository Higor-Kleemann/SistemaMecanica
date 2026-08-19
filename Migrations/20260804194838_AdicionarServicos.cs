using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaMecanica.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarServicos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensOS_Produtos_ProdutoId",
                table: "ItensOS");

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoId",
                table: "ItensOS",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "ItensOS",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServicoId",
                table: "ItensOS",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Servicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItensOS_ServicoId",
                table: "ItensOS",
                column: "ServicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensOS_Produtos_ProdutoId",
                table: "ItensOS",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensOS_Servicos_ServicoId",
                table: "ItensOS",
                column: "ServicoId",
                principalTable: "Servicos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensOS_Produtos_ProdutoId",
                table: "ItensOS");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensOS_Servicos_ServicoId",
                table: "ItensOS");

            migrationBuilder.DropTable(
                name: "Servicos");

            migrationBuilder.DropIndex(
                name: "IX_ItensOS_ServicoId",
                table: "ItensOS");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "ItensOS");

            migrationBuilder.DropColumn(
                name: "ServicoId",
                table: "ItensOS");

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoId",
                table: "ItensOS",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ItensOS_Produtos_ProdutoId",
                table: "ItensOS",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
