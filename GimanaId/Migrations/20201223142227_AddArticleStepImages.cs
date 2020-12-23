using Microsoft.EntityFrameworkCore.Migrations;

namespace GimanaIdApi.Migrations
{
    public partial class AddArticleStepImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image_Base64EncodedData",
                table: "ArticleStep",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image_FileFormat",
                table: "ArticleStep",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image_Base64EncodedData",
                table: "ArticleStep");

            migrationBuilder.DropColumn(
                name: "Image_FileFormat",
                table: "ArticleStep");
        }
    }
}
