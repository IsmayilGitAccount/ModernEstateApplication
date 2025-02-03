using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernEstateDemo.Migrations
{
    /// <inheritdoc />
    public partial class CreateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Exterior_ExteriorId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Parking_ParkingId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Roof_RoofId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_View_ViewId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyFeature_Feature_FeatureId",
                table: "PropertyFeature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_View",
                table: "View");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roof",
                table: "Roof");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parking",
                table: "Parking");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feature",
                table: "Feature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exterior",
                table: "Exterior");

            migrationBuilder.RenameTable(
                name: "View",
                newName: "Views");

            migrationBuilder.RenameTable(
                name: "Roof",
                newName: "Roofs");

            migrationBuilder.RenameTable(
                name: "Parking",
                newName: "Parkings");

            migrationBuilder.RenameTable(
                name: "Feature",
                newName: "Features");

            migrationBuilder.RenameTable(
                name: "Exterior",
                newName: "Exteriors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Views",
                table: "Views",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roofs",
                table: "Roofs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parkings",
                table: "Parkings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Features",
                table: "Features",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exteriors",
                table: "Exteriors",
                column: "Id");

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
                name: "FK_Properties_Views_ViewId",
                table: "Properties",
                column: "ViewId",
                principalTable: "Views",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyFeature_Features_FeatureId",
                table: "PropertyFeature",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_Properties_Views_ViewId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyFeature_Features_FeatureId",
                table: "PropertyFeature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Views",
                table: "Views");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roofs",
                table: "Roofs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parkings",
                table: "Parkings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Features",
                table: "Features");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exteriors",
                table: "Exteriors");

            migrationBuilder.RenameTable(
                name: "Views",
                newName: "View");

            migrationBuilder.RenameTable(
                name: "Roofs",
                newName: "Roof");

            migrationBuilder.RenameTable(
                name: "Parkings",
                newName: "Parking");

            migrationBuilder.RenameTable(
                name: "Features",
                newName: "Feature");

            migrationBuilder.RenameTable(
                name: "Exteriors",
                newName: "Exterior");

            migrationBuilder.AddPrimaryKey(
                name: "PK_View",
                table: "View",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roof",
                table: "Roof",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parking",
                table: "Parking",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feature",
                table: "Feature",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exterior",
                table: "Exterior",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Exterior_ExteriorId",
                table: "Properties",
                column: "ExteriorId",
                principalTable: "Exterior",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Parking_ParkingId",
                table: "Properties",
                column: "ParkingId",
                principalTable: "Parking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Roof_RoofId",
                table: "Properties",
                column: "RoofId",
                principalTable: "Roof",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_View_ViewId",
                table: "Properties",
                column: "ViewId",
                principalTable: "View",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyFeature_Feature_FeatureId",
                table: "PropertyFeature",
                column: "FeatureId",
                principalTable: "Feature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
