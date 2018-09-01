using Microsoft.EntityFrameworkCore.Migrations;

namespace ParityService.Migrations
{
    public partial class ManagedAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ManagedAccounts",
                columns: table => new
                {
                    ServiceLinkId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    AccountId = table.Column<string>(nullable: false),
                    AccountType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagedAccounts", x => new { x.AccountId, x.ServiceLinkId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ManagedAccounts_ServiceLinks_ServiceLinkId_UserId",
                        columns: x => new { x.ServiceLinkId, x.UserId },
                        principalTable: "ServiceLinks",
                        principalColumns: new[] { "Id", "UserId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ManagedAccounts_ServiceLinkId_UserId",
                table: "ManagedAccounts",
                columns: new[] { "ServiceLinkId", "UserId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManagedAccounts");
        }
    }
}
