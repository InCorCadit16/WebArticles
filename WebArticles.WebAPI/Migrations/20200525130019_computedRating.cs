using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class computedRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Articles");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEditDate",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEditDate",
                table: "Articles",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserArticleMark",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    ArticleId = table.Column<long>(nullable: false),
                    Mark = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserArticleMark", x => new { x.UserId, x.ArticleId });
                    table.ForeignKey(
                        name: "FK_UserArticleMark_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserArticleMark_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Auth",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserCommentMark",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    CommentId = table.Column<long>(nullable: false),
                    Mark = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCommentMark", x => new { x.UserId, x.CommentId });
                    table.ForeignKey(
                        name: "FK_UserCommentMark_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCommentMark_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Auth",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserArticleMark_ArticleId",
                table: "UserArticleMark",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCommentMark_CommentId",
                table: "UserCommentMark",
                column: "CommentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserArticleMark");

            migrationBuilder.DropTable(
                name: "UserCommentMark");

            migrationBuilder.DropColumn(
                name: "LastEditDate",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "LastEditDate",
                table: "Articles");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
