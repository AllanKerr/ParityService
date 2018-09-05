using Microsoft.EntityFrameworkCore.Migrations;

namespace ParityService.Migrations
{
    public partial class TargetPortfolio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TargetPortfolio",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetPortfolio", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_TargetPortfolio_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TargetAllocation",
                columns: table => new
                {
                    PortfolioUserId = table.Column<string>(nullable: false),
                    Symbol = table.Column<string>(nullable: false),
                    Proportion = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetAllocation", x => x.PortfolioUserId);
                    table.ForeignKey(
                        name: "FK_TargetAllocation_TargetPortfolio_PortfolioUserId",
                        column: x => x.PortfolioUserId,
                        principalTable: "TargetPortfolio",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TargetAllocation");

            migrationBuilder.DropTable(
                name: "TargetPortfolio");
        }
    }
}
