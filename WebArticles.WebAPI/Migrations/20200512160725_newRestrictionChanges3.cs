using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class newRestrictionChanges3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Writer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Reviewer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_Writer_UserId",
                table: "Writer",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Reviewer_UserId",
                table: "Reviewer",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviewer_Users_UserId",
                table: "Reviewer",
                column: "UserId",
                principalSchema: "Auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Writer_Users_UserId",
                table: "Writer",
                column: "UserId",
                principalSchema: "Auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
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

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Writer",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Reviewer",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

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
    }
}
