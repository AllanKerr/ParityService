using Microsoft.EntityFrameworkCore.Migrations;

namespace ParityService.Migrations
{
    public partial class Earnings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EarningsAppUserId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Earnings",
                columns: table => new
                {
                    AppUserId = table.Column<string>(nullable: false),
                    AnnualIncome = table.Column<decimal>(nullable: false),
                    Region = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Earnings", x => x.AppUserId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EarningsAppUserId",
                table: "AspNetUsers",
                column: "EarningsAppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Earnings_EarningsAppUserId",
                table: "AspNetUsers",
                column: "EarningsAppUserId",
                principalTable: "Earnings",
                principalColumn: "AppUserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Earnings_EarningsAppUserId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Earnings");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EarningsAppUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EarningsAppUserId",
                table: "AspNetUsers");
        }
    }
}
