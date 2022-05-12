using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MinTur.DataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class AdministratorAndTokenTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrators",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthenticationTokens",
                columns: table => new
                {
                    Token = table.Column<Guid>(nullable: false),
                    AdministratorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthenticationTokens", x => x.Token);
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthenticationTokens");

            migrationBuilder.DropTable(
                name: "Administrators");
        }
    }
}
