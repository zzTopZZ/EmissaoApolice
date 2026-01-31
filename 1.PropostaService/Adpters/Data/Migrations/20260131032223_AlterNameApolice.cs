using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterNameApolice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CApolices",
                table: "CApolices");

            migrationBuilder.RenameTable(
                name: "CApolices",
                newName: "Apolices");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Apolices",
                table: "Apolices",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Apolices",
                table: "Apolices");

            migrationBuilder.RenameTable(
                name: "Apolices",
                newName: "CApolices");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CApolices",
                table: "CApolices",
                column: "Id");
        }
    }
}
