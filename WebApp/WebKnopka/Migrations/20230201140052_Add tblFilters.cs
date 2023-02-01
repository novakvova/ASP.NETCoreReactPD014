using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebKnopka.Migrations
{
    /// <inheritdoc />
    public partial class AddtblFilters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblFilters",
                columns: table => new
                {
                    FilterValueId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFilters", x => new { x.ProductId, x.FilterValueId });
                    table.ForeignKey(
                        name: "FK_tblFilters_tblFilterValues_FilterValueId",
                        column: x => x.FilterValueId,
                        principalTable: "tblFilterValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblFilters_tblProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tblProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblFilters_FilterValueId",
                table: "tblFilters",
                column: "FilterValueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblFilters");
        }
    }
}
