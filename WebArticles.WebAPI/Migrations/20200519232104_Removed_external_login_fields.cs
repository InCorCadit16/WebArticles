using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class Removed_external_login_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviewer_Users_UserId",
                table: "Reviewer");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                schema: "Auth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Provider",
                schema: "Auth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PublichDate",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "PublichDate",
                table: "Articles");

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishDate",
                table: "Articles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Reviewer_Users_UserId",
                table: "Reviewer",
                column: "UserId",
                principalSchema: "Auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviewer_Users_UserId",
                table: "Reviewer");

            migrationBuilder.DropColumn(
                name: "PublishDate",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "PublishDate",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                schema: "Auth",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Provider",
                schema: "Auth",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublichDate",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PublichDate",
                table: "Articles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Reviewer_Users_UserId",
                table: "Reviewer",
                column: "UserId",
                principalSchema: "Auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
