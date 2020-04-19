using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendCodingTest.Migrations
{
    public partial class Initial_database_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserScores_UserScoreModelUserScoreID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserScoreModelUserScoreID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserScoreModelUserScoreID",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserScoreModelUserScoreID",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserScoreModelUserScoreID",
                table: "Users",
                column: "UserScoreModelUserScoreID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserScores_UserScoreModelUserScoreID",
                table: "Users",
                column: "UserScoreModelUserScoreID",
                principalTable: "UserScores",
                principalColumn: "UserScoreID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
