using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernEstateDemo.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Properties");
        }
    }
}
