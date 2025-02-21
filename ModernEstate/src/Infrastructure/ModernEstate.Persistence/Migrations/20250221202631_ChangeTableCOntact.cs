using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernEstate.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableCOntact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0);
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
