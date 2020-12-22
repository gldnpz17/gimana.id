using Microsoft.EntityFrameworkCore.Migrations;

namespace GimanaIdApi.Migrations
{
    public partial class AddNewAuthCriteria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IPAddress",
                table: "AuthTokens",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserAgent",
                table: "AuthTokens",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IPAddress",
                table: "AuthTokens");

            migrationBuilder.DropColumn(
                name: "UserAgent",
                table: "AuthTokens");
        }
    }
}
