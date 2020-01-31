using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoMy.Database.Migrations
{
    public partial class NewestMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_AccountId1",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_AccountId1",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "AccountId1",
                table: "Posts");

            migrationBuilder.AlterColumn<string>(
                name: "SenderAccountId",
                table: "Reports",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "Posts",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AccountId",
                table: "Posts",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_AccountId",
                table: "Posts",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_AccountId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_AccountId",
                table: "Posts");

            migrationBuilder.AlterColumn<int>(
                name: "SenderAccountId",
                table: "Reports",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "Posts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountId1",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AccountId1",
                table: "Posts",
                column: "AccountId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_AccountId1",
                table: "Posts",
                column: "AccountId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
