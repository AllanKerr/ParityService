using Microsoft.EntityFrameworkCore.Migrations;

namespace ParityService.Migrations
{
    public partial class ContributionRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ContributionRoom",
                table: "Accounts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContributionRoom",
                table: "Accounts");
        }
    }
}
