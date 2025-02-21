using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernEstate.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableContactTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Contacts");
        }
    }
}
