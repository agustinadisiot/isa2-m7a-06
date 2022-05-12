using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace MinTur.DataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class GuestsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdultsAmount",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "BabiesAmount",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "KidsAmount",
                table: "Accommodations");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "ReservationStates",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ReservationStates",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "GuestGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ammount = table.Column<int>(nullable: false),
                    GuestGroupType = table.Column<string>(nullable: false),
                    AccommodationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuestGroups_Accommodations_AccommodationId",
                        column: x => x.AccommodationId,
                        principalTable: "Accommodations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuestGroups_AccommodationId",
                table: "GuestGroups",
                column: "AccommodationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuestGroups");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "ReservationStates",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ReservationStates",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "AdultsAmount",
                table: "Accommodations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BabiesAmount",
                table: "Accommodations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KidsAmount",
                table: "Accommodations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
