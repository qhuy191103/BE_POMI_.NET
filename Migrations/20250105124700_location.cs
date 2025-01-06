using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace restapi.Migrations
{
    /// <inheritdoc />
    public partial class location : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Carts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Carts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Carts");
        }
    }
}
