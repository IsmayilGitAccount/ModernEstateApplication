using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernEstate.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Categories_CategoryId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Exteriors_ExteriorId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Parkings_ParkingId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Roofs_RoofId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Status_StatusId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Types_TypeId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Views_ViewId",
                table: "Properties");

            migrationBuilder.AlterColumn<int>(
                name: "ViewId",
                table: "Properties",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Properties",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Properties",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RoofId",
                table: "Properties",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ParkingId",
                table: "Properties",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ExteriorId",
                table: "Properties",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Properties",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AgentId",
                table: "Properties",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AgencyId",
                table: "Properties",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Agents",
                type: "nvarchar(1000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)");

            migrationBuilder.AlterColumn<int>(
                name: "AgencyId",
                table: "Agents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Agencies",
                type: "nvarchar(1000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Categories_CategoryId",
                table: "Properties",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Exteriors_ExteriorId",
                table: "Properties",
                column: "ExteriorId",
                principalTable: "Exteriors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Parkings_ParkingId",
                table: "Properties",
                column: "ParkingId",
                principalTable: "Parkings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Roofs_RoofId",
                table: "Properties",
                column: "RoofId",
                principalTable: "Roofs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Status_StatusId",
                table: "Properties",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Types_TypeId",
                table: "Properties",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Views_ViewId",
                table: "Properties",
                column: "ViewId",
                principalTable: "Views",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Categories_CategoryId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Exteriors_ExteriorId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Parkings_ParkingId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Roofs_RoofId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Status_StatusId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Types_TypeId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Views_ViewId",
                table: "Properties");

            migrationBuilder.AlterColumn<int>(
                name: "ViewId",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RoofId",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ParkingId",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ExteriorId",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AgentId",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AgencyId",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Agents",
                type: "nvarchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)");

            migrationBuilder.AlterColumn<int>(
                name: "AgencyId",
                table: "Agents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Agencies",
                type: "nvarchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Categories_CategoryId",
                table: "Properties",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Exteriors_ExteriorId",
                table: "Properties",
                column: "ExteriorId",
                principalTable: "Exteriors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Parkings_ParkingId",
                table: "Properties",
                column: "ParkingId",
                principalTable: "Parkings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Roofs_RoofId",
                table: "Properties",
                column: "RoofId",
                principalTable: "Roofs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Status_StatusId",
                table: "Properties",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Types_TypeId",
                table: "Properties",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Views_ViewId",
                table: "Properties",
                column: "ViewId",
                principalTable: "Views",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
