using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ForeyKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Propostas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Propostas_ClienteId",
                table: "Propostas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Apolices_PropostaId",
                table: "Apolices",
                column: "PropostaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apolices_Propostas_PropostaId",
                table: "Apolices",
                column: "PropostaId",
                principalTable: "Propostas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Propostas_Clientes_ClienteId",
                table: "Propostas",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apolices_Propostas_PropostaId",
                table: "Apolices");

            migrationBuilder.DropForeignKey(
                name: "FK_Propostas_Clientes_ClienteId",
                table: "Propostas");

            migrationBuilder.DropIndex(
                name: "IX_Propostas_ClienteId",
                table: "Propostas");

            migrationBuilder.DropIndex(
                name: "IX_Apolices_PropostaId",
                table: "Apolices");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Propostas");
        }
    }
}
