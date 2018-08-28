using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParityService.Migrations
{
    public partial class AccountLinks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LinkedAccounts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AppUserId = table.Column<string>(nullable: false),
                    IsPractice = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkedAccounts", x => new { x.Id, x.AppUserId });
                    table.ForeignKey(
                        name: "FK_LinkedAccounts_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Credentials",
                columns: table => new
                {
                    AccountLinkId = table.Column<string>(nullable: false),
                    AppUserId = table.Column<string>(nullable: false),
                    RefreshToken = table.Column<string>(nullable: true),
                    ApiServer = table.Column<string>(nullable: true),
                    AccessToken = table.Column<string>(nullable: true),
                    AccessTokenType = table.Column<string>(nullable: true),
                    AccessTokenExpiresAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credentials", x => new { x.AccountLinkId, x.AppUserId });
                    table.ForeignKey(
                        name: "FK_Credentials_LinkedAccounts_AccountLinkId_AppUserId",
                        columns: x => new { x.AccountLinkId, x.AppUserId },
                        principalTable: "LinkedAccounts",
                        principalColumns: new[] { "Id", "AppUserId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkedAccounts_AppUserId",
                table: "LinkedAccounts",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Credentials");

            migrationBuilder.DropTable(
                name: "LinkedAccounts");
        }
    }
}
