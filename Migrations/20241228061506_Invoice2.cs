using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace restapi.Migrations
{
    /// <inheritdoc />
    public partial class Invoice2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetails_Invoices_InvoiceId",
                table: "InvoiceDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetails_Product_ProductId",
                table: "InvoiceDetails");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceDetails_ProductId",
                table: "InvoiceDetails");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "InvoiceDetails",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceId",
                table: "InvoiceDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "InvoiceDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductImage",
                table: "InvoiceDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "InvoiceDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SelectedIce",
                table: "InvoiceDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SelectedSize",
                table: "InvoiceDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SelectedSugar",
                table: "InvoiceDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_Invoices_InvoiceId",
                table: "InvoiceDetails",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetails_Invoices_InvoiceId",
                table: "InvoiceDetails");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "InvoiceDetails");

            migrationBuilder.DropColumn(
                name: "ProductImage",
                table: "InvoiceDetails");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "InvoiceDetails");

            migrationBuilder.DropColumn(
                name: "SelectedIce",
                table: "InvoiceDetails");

            migrationBuilder.DropColumn(
                name: "SelectedSize",
                table: "InvoiceDetails");

            migrationBuilder.DropColumn(
                name: "SelectedSugar",
                table: "InvoiceDetails");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "InvoiceDetails",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceId",
                table: "InvoiceDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_ProductId",
                table: "InvoiceDetails",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_Invoices_InvoiceId",
                table: "InvoiceDetails",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_Product_ProductId",
                table: "InvoiceDetails",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
