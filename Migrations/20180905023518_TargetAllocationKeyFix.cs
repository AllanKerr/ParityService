using Microsoft.EntityFrameworkCore.Migrations;

namespace ParityService.Migrations
{
    public partial class TargetAllocationKeyFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TargetAllocations",
                table: "TargetAllocations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TargetAllocations",
                table: "TargetAllocations",
                columns: new[] { "PortfolioUserId", "Symbol" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TargetAllocations",
                table: "TargetAllocations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TargetAllocations",
                table: "TargetAllocations",
                column: "PortfolioUserId");
        }
    }
}
