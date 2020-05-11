using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class RatingIsComputed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WriterRating",
                table: "Writer");

            migrationBuilder.DropColumn(
                name: "ReviewerRating",
                table: "Reviewer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WriterRating",
                table: "Writer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReviewerRating",
                table: "Reviewer",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
