using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace MinTur.DataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class TouristPointCategoryTableRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TouristPointCategory_Categories_CategoryId",
                table: "TouristPointCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_TouristPointCategory_TouristPoints_TouristPointId",
                table: "TouristPointCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_TouristPoints_Images_ImageId",
                table: "TouristPoints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TouristPointCategory",
                table: "TouristPointCategory");

            migrationBuilder.RenameTable(
                name: "TouristPointCategory",
                newName: "TouristPointCategories");

            migrationBuilder.RenameIndex(
                name: "IX_TouristPointCategory_TouristPointId",
                table: "TouristPointCategories",
                newName: "IX_TouristPointCategories_TouristPointId");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "TouristPoints",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TouristPoints",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TouristPointCategories",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TouristPointCategories",
                table: "TouristPointCategories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TouristPointCategories_CategoryId_TouristPointId",
                table: "TouristPointCategories",
                columns: new[] { "CategoryId", "TouristPointId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TouristPointCategories_Categories_CategoryId",
                table: "TouristPointCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TouristPointCategories_TouristPoints_TouristPointId",
                table: "TouristPointCategories",
                column: "TouristPointId",
                principalTable: "TouristPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TouristPoints_Images_ImageId",
                table: "TouristPoints",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TouristPointCategories_Categories_CategoryId",
                table: "TouristPointCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_TouristPointCategories_TouristPoints_TouristPointId",
                table: "TouristPointCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_TouristPoints_Images_ImageId",
                table: "TouristPoints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TouristPointCategories",
                table: "TouristPointCategories");

            migrationBuilder.DropIndex(
                name: "IX_TouristPointCategories_CategoryId_TouristPointId",
                table: "TouristPointCategories");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TouristPointCategories");

            migrationBuilder.RenameTable(
                name: "TouristPointCategories",
                newName: "TouristPointCategory");

            migrationBuilder.RenameIndex(
                name: "IX_TouristPointCategories_TouristPointId",
                table: "TouristPointCategory",
                newName: "IX_TouristPointCategory_TouristPointId");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "TouristPoints",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TouristPoints",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 2000);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TouristPointCategory",
                table: "TouristPointCategory",
                columns: new[] { "CategoryId", "TouristPointId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TouristPointCategory_Categories_CategoryId",
                table: "TouristPointCategory",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TouristPointCategory_TouristPoints_TouristPointId",
                table: "TouristPointCategory",
                column: "TouristPointId",
                principalTable: "TouristPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TouristPoints_Images_ImageId",
                table: "TouristPoints",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
