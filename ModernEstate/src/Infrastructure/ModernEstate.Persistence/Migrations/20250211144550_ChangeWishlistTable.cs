using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernEstate.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeWishlistTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Wishlist_WishlistId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_WishlistId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "WishlistId",
                table: "Properties");

            migrationBuilder.AddColumn<int>(
                name: "PropertyId",
                table: "Wishlist",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_PropertyId",
                table: "Wishlist",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlist_Properties_PropertyId",
                table: "Wishlist",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishlist_Properties_PropertyId",
                table: "Wishlist");

            migrationBuilder.DropIndex(
                name: "IX_Wishlist_PropertyId",
                table: "Wishlist");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Wishlist");

            migrationBuilder.AddColumn<int>(
                name: "WishlistId",
                table: "Properties",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_WishlistId",
                table: "Properties",
                column: "WishlistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Wishlist_WishlistId",
                table: "Properties",
                column: "WishlistId",
                principalTable: "Wishlist",
                principalColumn: "Id");
        }
    }
}
