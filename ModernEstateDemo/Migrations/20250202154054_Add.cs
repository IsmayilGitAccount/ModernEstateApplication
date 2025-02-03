using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernEstateDemo.Migrations
{
    /// <inheritdoc />
    public partial class Add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Property");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Property",
                type: "nvarchar(200)",
                nullable: false,
                defaultValue: "");
        }
    }
}
