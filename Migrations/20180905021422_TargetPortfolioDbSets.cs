using Microsoft.EntityFrameworkCore.Migrations;

namespace ParityService.Migrations
{
    public partial class TargetPortfolioDbSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TargetAllocation_TargetPortfolio_PortfolioUserId",
                table: "TargetAllocation");

            migrationBuilder.DropForeignKey(
                name: "FK_TargetPortfolio_AspNetUsers_UserId",
                table: "TargetPortfolio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TargetPortfolio",
                table: "TargetPortfolio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TargetAllocation",
                table: "TargetAllocation");

            migrationBuilder.RenameTable(
                name: "TargetPortfolio",
                newName: "TargetPortfolios");

            migrationBuilder.RenameTable(
                name: "TargetAllocation",
                newName: "TargetAllocations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TargetPortfolios",
                table: "TargetPortfolios",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TargetAllocations",
                table: "TargetAllocations",
                column: "PortfolioUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TargetAllocations_TargetPortfolios_PortfolioUserId",
                table: "TargetAllocations",
                column: "PortfolioUserId",
                principalTable: "TargetPortfolios",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TargetPortfolios_AspNetUsers_UserId",
                table: "TargetPortfolios",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TargetAllocations_TargetPortfolios_PortfolioUserId",
                table: "TargetAllocations");

            migrationBuilder.DropForeignKey(
                name: "FK_TargetPortfolios_AspNetUsers_UserId",
                table: "TargetPortfolios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TargetPortfolios",
                table: "TargetPortfolios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TargetAllocations",
                table: "TargetAllocations");

            migrationBuilder.RenameTable(
                name: "TargetPortfolios",
                newName: "TargetPortfolio");

            migrationBuilder.RenameTable(
                name: "TargetAllocations",
                newName: "TargetAllocation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TargetPortfolio",
                table: "TargetPortfolio",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TargetAllocation",
                table: "TargetAllocation",
                column: "PortfolioUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TargetAllocation_TargetPortfolio_PortfolioUserId",
                table: "TargetAllocation",
                column: "PortfolioUserId",
                principalTable: "TargetPortfolio",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TargetPortfolio_AspNetUsers_UserId",
                table: "TargetPortfolio",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
