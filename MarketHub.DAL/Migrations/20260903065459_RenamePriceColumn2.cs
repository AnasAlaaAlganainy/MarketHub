using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketHub.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RenamePriceColumn2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "product_price",
                table: "Products",
                newName: "Price");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "product_price");
        }
    }
}
