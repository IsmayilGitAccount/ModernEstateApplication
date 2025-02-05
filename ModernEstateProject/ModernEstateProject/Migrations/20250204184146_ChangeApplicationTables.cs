using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernEstateProject.Migrations
{
    /// <inheritdoc />
    public partial class ChangeApplicationTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Properties");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
