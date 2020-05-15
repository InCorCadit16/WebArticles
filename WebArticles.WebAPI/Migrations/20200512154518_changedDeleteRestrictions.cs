using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class changedDeleteRestrictions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviewer_Users_UserId",
                table: "Reviewer");

            migrationBuilder.DropForeignKey(
                name: "FK_Writer_Users_UserId",
                table: "Writer");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Reviewer_Users_UserId",
                table: "Reviewer",
                column: "UserId",
                principalSchema: "Auth",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Writer_Users_UserId",
                table: "Writer",
                column: "UserId",
                principalSchema: "Auth",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
