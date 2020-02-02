using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoMy.Database.Migrations
{
    public partial class ActivationCodeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActivationCode",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivationCode",
                table: "AspNetUsers");
        }
    }
}
