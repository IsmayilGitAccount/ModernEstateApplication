using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernEstateDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryTablesPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryPhoto",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryPhoto",
                table: "Categories");
        }
    }
}
