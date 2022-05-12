using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MinTur.DataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class MemberSinceColumnInReservationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Resorts_ResortId",
                table: "Reviews");

            migrationBuilder.AlterColumn<int>(
                name: "ResortId",
                table: "Reviews",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MemberSince",
                table: "Resorts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Resorts_ResortId",
                table: "Reviews",
                column: "ResortId",
                principalTable: "Resorts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Resorts_ResortId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "MemberSince",
                table: "Resorts");

            migrationBuilder.AlterColumn<int>(
                name: "ResortId",
                table: "Reviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Resorts_ResortId",
                table: "Reviews",
                column: "ResortId",
                principalTable: "Resorts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
