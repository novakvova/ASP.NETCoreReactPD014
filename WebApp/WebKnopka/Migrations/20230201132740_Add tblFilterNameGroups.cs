using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebKnopka.Migrations
{
    /// <inheritdoc />
    public partial class AddtblFilterNameGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblFilterNameGroups",
                columns: table => new
                {
                    FilterNameId = table.Column<int>(type: "integer", nullable: false),
                    FilterValueId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFilterNameGroups", x => new { x.FilterNameId, x.FilterValueId });
                    table.ForeignKey(
                        name: "FK_tblFilterNameGroups_tblFilterNames_FilterNameId",
                        column: x => x.FilterNameId,
                        principalTable: "tblFilterNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblFilterNameGroups_tblFilterValues_FilterValueId",
                        column: x => x.FilterValueId,
                        principalTable: "tblFilterValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblFilterNameGroups_FilterValueId",
                table: "tblFilterNameGroups",
                column: "FilterValueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblFilterNameGroups");
        }
    }
}
