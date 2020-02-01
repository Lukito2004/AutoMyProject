using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoMy.Database.Migrations
{
    public partial class PostMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CityName",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TechInspection",
                table: "Posts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "buyType",
                table: "Posts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityName",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "TechInspection",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "buyType",
                table: "Posts");
        }
    }
}
