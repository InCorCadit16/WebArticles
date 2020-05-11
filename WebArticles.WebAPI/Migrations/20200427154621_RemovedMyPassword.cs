using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class RemovedMyPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Reviewer_ReviewerId",
                schema: "Auth",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Writer_WriterId",
                schema: "Auth",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ReviewerId",
                schema: "Auth",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_WriterId",
                schema: "Auth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Password",
                schema: "Auth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ReviewerId",
                schema: "Auth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WriterId",
                schema: "Auth",
                table: "Users");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Writer",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Reviewer",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Writer_UserId",
                table: "Writer",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviewer_UserId",
                table: "Reviewer",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviewer_Users_UserId",
                table: "Reviewer",
                column: "UserId",
                principalSchema: "Auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Writer_Users_UserId",
                table: "Writer",
                column: "UserId",
                principalSchema: "Auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviewer_Users_UserId",
                table: "Reviewer");

            migrationBuilder.DropForeignKey(
                name: "FK_Writer_Users_UserId",
                table: "Writer");

            migrationBuilder.DropIndex(
                name: "IX_Writer_UserId",
                table: "Writer");

            migrationBuilder.DropIndex(
                name: "IX_Reviewer_UserId",
                table: "Reviewer");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Writer");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reviewer");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                schema: "Auth",
                table: "Users",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "ReviewerId",
                schema: "Auth",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "WriterId",
                schema: "Auth",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ReviewerId",
                schema: "Auth",
                table: "Users",
                column: "ReviewerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_WriterId",
                schema: "Auth",
                table: "Users",
                column: "WriterId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Reviewer_ReviewerId",
                schema: "Auth",
                table: "Users",
                column: "ReviewerId",
                principalTable: "Reviewer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Writer_WriterId",
                schema: "Auth",
                table: "Users",
                column: "WriterId",
                principalTable: "Writer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
