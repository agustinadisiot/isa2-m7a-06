using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace MinTur.DataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class ReviewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ammount",
                table: "GuestGroups");

            migrationBuilder.AddColumn<double>(
                name: "Punctuation",
                table: "Resorts",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "GuestGroups",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(nullable: false),
                    Stars = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Surname = table.Column<string>(nullable: false),
                    ResortId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Resorts_ResortId",
                        column: x => x.ResortId,
                        principalTable: "Resorts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ResortId",
                table: "Reviews",
                column: "ResortId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropColumn(
                name: "Punctuation",
                table: "Resorts");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "GuestGroups");

            migrationBuilder.AddColumn<int>(
                name: "Ammount",
                table: "GuestGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
