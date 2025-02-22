using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernEstate.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Contacts");
        }
    }
}
