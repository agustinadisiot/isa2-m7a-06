using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MinTur.DataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class TokensNowHaveDateValidSince : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthenticationTokens",
                table: "AuthenticationTokens");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "AuthenticationTokens");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "AuthenticationTokens",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidSince",
                table: "AuthenticationTokens",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthenticationTokens",
                table: "AuthenticationTokens",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthenticationTokens",
                table: "AuthenticationTokens");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AuthenticationTokens");

            migrationBuilder.DropColumn(
                name: "ValidSince",
                table: "AuthenticationTokens");

            migrationBuilder.AddColumn<Guid>(
                name: "Token",
                table: "AuthenticationTokens",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthenticationTokens",
                table: "AuthenticationTokens",
                column: "Token");
        }
    }
}
