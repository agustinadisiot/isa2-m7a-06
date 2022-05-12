using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace MinTur.DataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class ReservationStateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Reservations");

            migrationBuilder.AddColumn<int>(
                name: "ActualStateId",
                table: "Reservations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ReservationStates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationStates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ActualStateId",
                table: "Reservations",
                column: "ActualStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ReservationStates_ActualStateId",
                table: "Reservations",
                column: "ActualStateId",
                principalTable: "ReservationStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ReservationStates_ActualStateId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "ReservationStates");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ActualStateId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ActualStateId",
                table: "Reservations");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
