using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketHub.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RenamePriceColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "price",
                table: "Products",
                newName: "product_price");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "product_price",
                table: "Products",
                newName: "price");
        }
    }
}
