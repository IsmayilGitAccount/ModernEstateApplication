using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernEstateDemo.Migrations
{
    /// <inheritdoc />
    public partial class Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgencyName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exterior",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExteriorType = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exterior", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feature",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeatureName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feature", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParkingType = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parking", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roof",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoofType = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roof", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "View",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViewType = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_View", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Agent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    FacebookLink = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    XLink = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LinkedinLink = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    InstagramLink = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    AgencyId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agent_Agency_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Property",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Area = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BedroomCount = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    BathroomCount = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    GarageCount = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    BuiltYear = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LotSize = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SchoolDistrict = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    RoomCount = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    AgentId = table.Column<int>(type: "int", nullable: false),
                    AgencyId = table.Column<int>(type: "int", nullable: false),
                    ParkingId = table.Column<int>(type: "int", nullable: false),
                    RoofId = table.Column<int>(type: "int", nullable: false),
                    ViewId = table.Column<int>(type: "int", nullable: false),
                    ExteriorId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Property_Agency_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Property_Agent_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Property_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Property_Exterior_ExteriorId",
                        column: x => x.ExteriorId,
                        principalTable: "Exterior",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Property_Parking_ParkingId",
                        column: x => x.ParkingId,
                        principalTable: "Parking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Property_Roof_RoofId",
                        column: x => x.RoofId,
                        principalTable: "Roof",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Property_View_ViewId",
                        column: x => x.ViewId,
                        principalTable: "View",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertiesPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Photo = table.Column<string>(type: "char(200)", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertiesPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertiesPhoto_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyFeature",
                columns: table => new
                {
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    FeatureId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyFeature", x => new { x.PropertyId, x.FeatureId });
                    table.ForeignKey(
                        name: "FK_PropertyFeature_Feature_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Feature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyFeature_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agent_AgencyId",
                table: "Agent",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertiesPhoto_Photo",
                table: "PropertiesPhoto",
                column: "Photo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertiesPhoto_PropertyId",
                table: "PropertiesPhoto",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Property_AgencyId",
                table: "Property",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Property_AgentId",
                table: "Property",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Property_CategoryId",
                table: "Property",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Property_ExteriorId",
                table: "Property",
                column: "ExteriorId");

            migrationBuilder.CreateIndex(
                name: "IX_Property_ParkingId",
                table: "Property",
                column: "ParkingId");

            migrationBuilder.CreateIndex(
                name: "IX_Property_RoofId",
                table: "Property",
                column: "RoofId");

            migrationBuilder.CreateIndex(
                name: "IX_Property_ViewId",
                table: "Property",
                column: "ViewId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyFeature_FeatureId",
                table: "PropertyFeature",
                column: "FeatureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertiesPhoto");

            migrationBuilder.DropTable(
                name: "PropertyFeature");

            migrationBuilder.DropTable(
                name: "Feature");

            migrationBuilder.DropTable(
                name: "Property");

            migrationBuilder.DropTable(
                name: "Agent");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Exterior");

            migrationBuilder.DropTable(
                name: "Parking");

            migrationBuilder.DropTable(
                name: "Roof");

            migrationBuilder.DropTable(
                name: "View");

            migrationBuilder.DropTable(
                name: "Agency");
        }
    }
}
