using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    public partial class newdataseeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_FurnishTypes_FurnishTypeId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_PropertyTypes_ProperTypeId",
                table: "Properties");

            migrationBuilder.DropTable(
                name: "FurnishTypes");

            migrationBuilder.DropIndex(
                name: "IX_Properties_ProperTypeId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "ProperTypeId",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "TotalFloor",
                table: "Properties",
                newName: "TotalFloors");

            migrationBuilder.RenameColumn(
                name: "FurnishTypeId",
                table: "Properties",
                newName: "FurnishingTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_FurnishTypeId",
                table: "Properties",
                newName: "IX_Properties_FurnishingTypeId");

            migrationBuilder.CreateTable(
                name: "FurnishingTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FurnishingTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PropertyTypeId",
                table: "Properties",
                column: "PropertyTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_FurnishingTypes_FurnishingTypeId",
                table: "Properties",
                column: "FurnishingTypeId",
                principalTable: "FurnishingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_PropertyTypes_PropertyTypeId",
                table: "Properties",
                column: "PropertyTypeId",
                principalTable: "PropertyTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_FurnishingTypes_FurnishingTypeId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_PropertyTypes_PropertyTypeId",
                table: "Properties");

            migrationBuilder.DropTable(
                name: "FurnishingTypes");

            migrationBuilder.DropIndex(
                name: "IX_Properties_PropertyTypeId",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "TotalFloors",
                table: "Properties",
                newName: "TotalFloor");

            migrationBuilder.RenameColumn(
                name: "FurnishingTypeId",
                table: "Properties",
                newName: "FurnishTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_FurnishingTypeId",
                table: "Properties",
                newName: "IX_Properties_FurnishTypeId");

            migrationBuilder.AddColumn<int>(
                name: "ProperTypeId",
                table: "Properties",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FurnishTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FurnishTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_ProperTypeId",
                table: "Properties",
                column: "ProperTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_FurnishTypes_FurnishTypeId",
                table: "Properties",
                column: "FurnishTypeId",
                principalTable: "FurnishTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_PropertyTypes_ProperTypeId",
                table: "Properties",
                column: "ProperTypeId",
                principalTable: "PropertyTypes",
                principalColumn: "Id");
        }
    }
}
