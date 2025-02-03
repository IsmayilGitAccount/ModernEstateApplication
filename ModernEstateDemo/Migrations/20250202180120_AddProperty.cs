using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernEstateDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agent_Agency_AgencyId",
                table: "Agent");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertiesPhoto_Property_PropertyId",
                table: "PropertiesPhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_Agency_AgencyId",
                table: "Property");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_Agent_AgentId",
                table: "Property");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_Category_CategoryId",
                table: "Property");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_Exterior_ExteriorId",
                table: "Property");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_Parking_ParkingId",
                table: "Property");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_Roof_RoofId",
                table: "Property");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_View_ViewId",
                table: "Property");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyFeature_Property_PropertyId",
                table: "PropertyFeature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Property",
                table: "Property");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertiesPhoto",
                table: "PropertiesPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agent",
                table: "Agent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agency",
                table: "Agency");

            migrationBuilder.RenameTable(
                name: "Property",
                newName: "Properties");

            migrationBuilder.RenameTable(
                name: "PropertiesPhoto",
                newName: "PropertiesPhotos");

            migrationBuilder.RenameTable(
                name: "Agent",
                newName: "Agents");

            migrationBuilder.RenameTable(
                name: "Agency",
                newName: "Agencies");

            migrationBuilder.RenameIndex(
                name: "IX_Property_ViewId",
                table: "Properties",
                newName: "IX_Properties_ViewId");

            migrationBuilder.RenameIndex(
                name: "IX_Property_RoofId",
                table: "Properties",
                newName: "IX_Properties_RoofId");

            migrationBuilder.RenameIndex(
                name: "IX_Property_ParkingId",
                table: "Properties",
                newName: "IX_Properties_ParkingId");

            migrationBuilder.RenameIndex(
                name: "IX_Property_ExteriorId",
                table: "Properties",
                newName: "IX_Properties_ExteriorId");

            migrationBuilder.RenameIndex(
                name: "IX_Property_CategoryId",
                table: "Properties",
                newName: "IX_Properties_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Property_AgentId",
                table: "Properties",
                newName: "IX_Properties_AgentId");

            migrationBuilder.RenameIndex(
                name: "IX_Property_AgencyId",
                table: "Properties",
                newName: "IX_Properties_AgencyId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertiesPhoto_PropertyId",
                table: "PropertiesPhotos",
                newName: "IX_PropertiesPhotos_PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertiesPhoto_Photo",
                table: "PropertiesPhotos",
                newName: "IX_PropertiesPhotos_Photo");

            migrationBuilder.RenameIndex(
                name: "IX_Agent_AgencyId",
                table: "Agents",
                newName: "IX_Agents_AgencyId");

            migrationBuilder.AlterColumn<int>(
                name: "ExteriorId",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Properties",
                table: "Properties",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertiesPhotos",
                table: "PropertiesPhotos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agents",
                table: "Agents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agencies",
                table: "Agencies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_Agencies_AgencyId",
                table: "Agents",
                column: "AgencyId",
                principalTable: "Agencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Agencies_AgencyId",
                table: "Properties",
                column: "AgencyId",
                principalTable: "Agencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Agents_AgentId",
                table: "Properties",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Category_CategoryId",
                table: "Properties",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_PropertiesPhotos_Properties_PropertyId",
                table: "PropertiesPhotos",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyFeature_Properties_PropertyId",
                table: "PropertyFeature",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agents_Agencies_AgencyId",
                table: "Agents");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Agencies_AgencyId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Agents_AgentId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Category_CategoryId",
                table: "Properties");

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
                name: "FK_PropertiesPhotos_Properties_PropertyId",
                table: "PropertiesPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyFeature_Properties_PropertyId",
                table: "PropertyFeature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertiesPhotos",
                table: "PropertiesPhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Properties",
                table: "Properties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agents",
                table: "Agents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agencies",
                table: "Agencies");

            migrationBuilder.RenameTable(
                name: "PropertiesPhotos",
                newName: "PropertiesPhoto");

            migrationBuilder.RenameTable(
                name: "Properties",
                newName: "Property");

            migrationBuilder.RenameTable(
                name: "Agents",
                newName: "Agent");

            migrationBuilder.RenameTable(
                name: "Agencies",
                newName: "Agency");

            migrationBuilder.RenameIndex(
                name: "IX_PropertiesPhotos_PropertyId",
                table: "PropertiesPhoto",
                newName: "IX_PropertiesPhoto_PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertiesPhotos_Photo",
                table: "PropertiesPhoto",
                newName: "IX_PropertiesPhoto_Photo");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_ViewId",
                table: "Property",
                newName: "IX_Property_ViewId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_RoofId",
                table: "Property",
                newName: "IX_Property_RoofId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_ParkingId",
                table: "Property",
                newName: "IX_Property_ParkingId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_ExteriorId",
                table: "Property",
                newName: "IX_Property_ExteriorId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_CategoryId",
                table: "Property",
                newName: "IX_Property_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_AgentId",
                table: "Property",
                newName: "IX_Property_AgentId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_AgencyId",
                table: "Property",
                newName: "IX_Property_AgencyId");

            migrationBuilder.RenameIndex(
                name: "IX_Agents_AgencyId",
                table: "Agent",
                newName: "IX_Agent_AgencyId");

            migrationBuilder.AlterColumn<int>(
                name: "ExteriorId",
                table: "Property",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertiesPhoto",
                table: "PropertiesPhoto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Property",
                table: "Property",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agent",
                table: "Agent",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agency",
                table: "Agency",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Agent_Agency_AgencyId",
                table: "Agent",
                column: "AgencyId",
                principalTable: "Agency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertiesPhoto_Property_PropertyId",
                table: "PropertiesPhoto",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Agency_AgencyId",
                table: "Property",
                column: "AgencyId",
                principalTable: "Agency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Agent_AgentId",
                table: "Property",
                column: "AgentId",
                principalTable: "Agent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Category_CategoryId",
                table: "Property",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Exterior_ExteriorId",
                table: "Property",
                column: "ExteriorId",
                principalTable: "Exterior",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Parking_ParkingId",
                table: "Property",
                column: "ParkingId",
                principalTable: "Parking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Roof_RoofId",
                table: "Property",
                column: "RoofId",
                principalTable: "Roof",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Property_View_ViewId",
                table: "Property",
                column: "ViewId",
                principalTable: "View",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyFeature_Property_PropertyId",
                table: "PropertyFeature",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
