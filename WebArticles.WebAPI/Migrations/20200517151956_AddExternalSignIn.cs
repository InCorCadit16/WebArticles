using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class AddExternalSignIn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                schema: "Auth",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Provider",
                schema: "Auth",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalId",
                schema: "Auth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Provider",
                schema: "Auth",
                table: "Users");
        }
    }
}
