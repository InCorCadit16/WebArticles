using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class AddTopicsDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Topic_TopicId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewerTopic_Topic_TopicId",
                table: "ReviewerTopic");

            migrationBuilder.DropForeignKey(
                name: "FK_WriterTopic_Topic_TopicId",
                table: "WriterTopic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topic",
                table: "Topic");

            migrationBuilder.RenameTable(
                name: "Topic",
                newName: "Topics");

            migrationBuilder.RenameIndex(
                name: "IX_Topic_TopicName",
                table: "Topics",
                newName: "IX_Topics_TopicName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topics",
                table: "Topics",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Topics_TopicId",
                table: "Articles",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewerTopic_Topics_TopicId",
                table: "ReviewerTopic",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WriterTopic_Topics_TopicId",
                table: "WriterTopic",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Topics_TopicId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewerTopic_Topics_TopicId",
                table: "ReviewerTopic");

            migrationBuilder.DropForeignKey(
                name: "FK_WriterTopic_Topics_TopicId",
                table: "WriterTopic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topics",
                table: "Topics");

            migrationBuilder.RenameTable(
                name: "Topics",
                newName: "Topic");

            migrationBuilder.RenameIndex(
                name: "IX_Topics_TopicName",
                table: "Topic",
                newName: "IX_Topic_TopicName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topic",
                table: "Topic",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Topic_TopicId",
                table: "Articles",
                column: "TopicId",
                principalTable: "Topic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewerTopic_Topic_TopicId",
                table: "ReviewerTopic",
                column: "TopicId",
                principalTable: "Topic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WriterTopic_Topic_TopicId",
                table: "WriterTopic",
                column: "TopicId",
                principalTable: "Topic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
