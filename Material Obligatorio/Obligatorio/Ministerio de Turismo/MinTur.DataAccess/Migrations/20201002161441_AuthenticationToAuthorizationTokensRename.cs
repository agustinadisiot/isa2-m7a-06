using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MinTur.DataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class AuthenticationToAuthorizationTokensRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthenticationTokens");

            migrationBuilder.CreateTable(
                name: "AuthorizationTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AdministratorId = table.Column<int>(nullable: false),
                    ValidSince = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorizationTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthorizationTokens_Administrators_AdministratorId",
                        column: x => x.AdministratorId,
                        principalTable: "Administrators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorizationTokens_AdministratorId",
                table: "AuthorizationTokens",
                column: "AdministratorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorizationTokens");

            migrationBuilder.CreateTable(
                name: "AuthenticationTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdministratorId = table.Column<int>(type: "int", nullable: false),
                    ValidSince = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthenticationTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthenticationTokens_Administrators_AdministratorId",
                        column: x => x.AdministratorId,
                        principalTable: "Administrators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthenticationTokens_AdministratorId",
                table: "AuthenticationTokens",
                column: "AdministratorId");
        }
    }
}
