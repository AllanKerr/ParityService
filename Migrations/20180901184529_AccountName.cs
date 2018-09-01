using Microsoft.EntityFrameworkCore.Migrations;

namespace ParityService.Migrations
{
    public partial class AccountName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Accounts",
                newName: "AccountName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountName",
                table: "Accounts",
                newName: "AccountId");
        }
    }
}
