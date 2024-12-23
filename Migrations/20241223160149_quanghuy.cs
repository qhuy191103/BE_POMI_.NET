using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace restapi.Migrations
{
    /// <inheritdoc />
    public partial class quanghuy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductDetail");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Product",
                newName: "Quantity");

            migrationBuilder.AddColumn<string>(
                name: "IceLevels",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sizes",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "SugarLevels",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IceLevels",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Sizes",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SugarLevels",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Product",
                newName: "ProductId");

            migrationBuilder.CreateTable(
                name: "ProductDetail",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    IceLevels = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Sizes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SugarLevels = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetail", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_ProductDetail_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
