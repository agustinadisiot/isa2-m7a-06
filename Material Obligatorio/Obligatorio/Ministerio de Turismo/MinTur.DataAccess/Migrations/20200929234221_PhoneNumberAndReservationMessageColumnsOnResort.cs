using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace MinTur.DataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class PhoneNumberAndReservationMessageColumnsOnResort : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Resorts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReservationMessage",
                table: "Resorts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Resorts");

            migrationBuilder.DropColumn(
                name: "ReservationMessage",
                table: "Resorts");
        }
    }
}
