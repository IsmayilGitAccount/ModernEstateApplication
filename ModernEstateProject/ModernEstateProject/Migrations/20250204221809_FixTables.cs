using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernEstateProject.Migrations
{
    /// <inheritdoc />
    public partial class FixTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgencyId1",
                table: "Agents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Agents_AgencyId1",
                table: "Agents",
                column: "AgencyId1",
                unique: true,
                filter: "[AgencyId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_Agencies_AgencyId1",
                table: "Agents",
                column: "AgencyId1",
                principalTable: "Agencies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agents_Agencies_AgencyId1",
                table: "Agents");

            migrationBuilder.DropIndex(
                name: "IX_Agents_AgencyId1",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "AgencyId1",
                table: "Agents");
        }
    }
}
