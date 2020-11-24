using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GimanaIdApi.Migrations
{
    public partial class DatabaseInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    HeroImage_FileFormat = table.Column<string>(type: "text", nullable: true),
                    HeroImage_Base64EncodedData = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailVerificationToken",
                columns: table => new
                {
                    Token = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailVerificationToken", x => x.Token);
                });

            migrationBuilder.CreateTable(
                name: "PasswordResetToken",
                columns: table => new
                {
                    Token = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordResetToken", x => x.Token);
                });

            migrationBuilder.CreateTable(
                name: "ArticleHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    HeroImage_FileFormat = table.Column<string>(type: "text", nullable: true),
                    HeroImage_Base64EncodedData = table.Column<string>(type: "text", nullable: true),
                    ArticleId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleHistory_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArticlePart",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    PartNumber = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ArticleHistoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    ArticleId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticlePart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticlePart_ArticleHistory_ArticleHistoryId",
                        column: x => x.ArticleHistoryId,
                        principalTable: "ArticleHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArticlePart_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Username = table.Column<string>(type: "text", nullable: true),
                    ProfilePicture_FileFormat = table.Column<string>(type: "text", nullable: true),
                    ProfilePicture_Base64EncodedData = table.Column<string>(type: "text", nullable: true),
                    BanLiftedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ArticleHistoryId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_ArticleHistory_ArticleHistoryId",
                        column: x => x.ArticleHistoryId,
                        principalTable: "ArticleHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArticleStep",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    StepNumber = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ArticlePartId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleStep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleStep_ArticlePart_ArticlePartId",
                        column: x => x.ArticlePartId,
                        principalTable: "ArticlePart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArticleIssue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    ArticleId = table.Column<Guid>(type: "uuid", nullable: true),
                    SubmitterId = table.Column<Guid>(type: "uuid", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    Resolved = table.Column<bool>(type: "boolean", nullable: false),
                    ArticleHistoryId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleIssue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleIssue_ArticleHistory_ArticleHistoryId",
                        column: x => x.ArticleHistoryId,
                        principalTable: "ArticleHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArticleIssue_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArticleIssue_Users_SubmitterId",
                        column: x => x.SubmitterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArticleRating",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    ArticleId = table.Column<Guid>(type: "uuid", nullable: true),
                    SubmitterId = table.Column<Guid>(type: "uuid", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    ArticleHistoryId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleRating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleRating_ArticleHistory_ArticleHistoryId",
                        column: x => x.ArticleHistoryId,
                        principalTable: "ArticleHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArticleRating_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArticleRating_Users_SubmitterId",
                        column: x => x.SubmitterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArticleUser",
                columns: table => new
                {
                    ArticlesId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleUser", x => new { x.ArticlesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ArticleUser_Articles_ArticlesId",
                        column: x => x.ArticlesId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthTokens",
                columns: table => new
                {
                    Token = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthTokens", x => x.Token);
                    table.ForeignKey(
                        name: "FK_AuthTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PasswordCredential",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    HashedPassword = table.Column<string>(type: "text", nullable: true),
                    PasswordSalt = table.Column<string>(type: "text", nullable: true),
                    PasswordResetTokenToken = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordCredential", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_PasswordCredential_PasswordResetToken_PasswordResetTokenTok~",
                        column: x => x.PasswordResetTokenToken,
                        principalTable: "PasswordResetToken",
                        principalColumn: "Token",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PasswordCredential_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserEmail",
                columns: table => new
                {
                    EmailAddress = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    IsVerified = table.Column<bool>(type: "boolean", nullable: false),
                    VerificationTokenToken = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEmail", x => x.EmailAddress);
                    table.ForeignKey(
                        name: "FK_UserEmail_EmailVerificationToken_VerificationTokenToken",
                        column: x => x.VerificationTokenToken,
                        principalTable: "EmailVerificationToken",
                        principalColumn: "Token",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserEmail_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserEmail_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserPrivilege",
                columns: table => new
                {
                    PrivilegeName = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPrivilege", x => x.PrivilegeName);
                    table.ForeignKey(
                        name: "FK_UserPrivilege_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleHistory_ArticleId",
                table: "ArticleHistory",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleIssue_ArticleHistoryId",
                table: "ArticleIssue",
                column: "ArticleHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleIssue_ArticleId",
                table: "ArticleIssue",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleIssue_SubmitterId",
                table: "ArticleIssue",
                column: "SubmitterId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticlePart_ArticleHistoryId",
                table: "ArticlePart",
                column: "ArticleHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticlePart_ArticleId",
                table: "ArticlePart",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleRating_ArticleHistoryId",
                table: "ArticleRating",
                column: "ArticleHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleRating_ArticleId",
                table: "ArticleRating",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleRating_SubmitterId",
                table: "ArticleRating",
                column: "SubmitterId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleStep_ArticlePartId",
                table: "ArticleStep",
                column: "ArticlePartId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleUser_UsersId",
                table: "ArticleUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthTokens_UserId",
                table: "AuthTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordCredential_PasswordResetTokenToken",
                table: "PasswordCredential",
                column: "PasswordResetTokenToken");

            migrationBuilder.CreateIndex(
                name: "IX_UserEmail_UserId",
                table: "UserEmail",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserEmail_UserId1",
                table: "UserEmail",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserEmail_VerificationTokenToken",
                table: "UserEmail",
                column: "VerificationTokenToken");

            migrationBuilder.CreateIndex(
                name: "IX_UserPrivilege_UserId",
                table: "UserPrivilege",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ArticleHistoryId",
                table: "Users",
                column: "ArticleHistoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleIssue");

            migrationBuilder.DropTable(
                name: "ArticleRating");

            migrationBuilder.DropTable(
                name: "ArticleStep");

            migrationBuilder.DropTable(
                name: "ArticleUser");

            migrationBuilder.DropTable(
                name: "AuthTokens");

            migrationBuilder.DropTable(
                name: "PasswordCredential");

            migrationBuilder.DropTable(
                name: "UserEmail");

            migrationBuilder.DropTable(
                name: "UserPrivilege");

            migrationBuilder.DropTable(
                name: "ArticlePart");

            migrationBuilder.DropTable(
                name: "PasswordResetToken");

            migrationBuilder.DropTable(
                name: "EmailVerificationToken");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ArticleHistory");

            migrationBuilder.DropTable(
                name: "Articles");
        }
    }
}
