using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendCodingTest.Migrations
{
    public partial class Initial_Database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserScores",
                columns: table => new
                {
                    UserScoreID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserScores", x => x.UserScoreID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: false),
                    UserScoreModelUserScoreID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_UserScores_UserScoreModelUserScoreID",
                        column: x => x.UserScoreModelUserScoreID,
                        principalTable: "UserScores",
                        principalColumn: "UserScoreID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserScoreModelUserScoreID",
                table: "Users",
                column: "UserScoreModelUserScoreID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserScores_UserID",
                table: "UserScores",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserScores_Users_UserID",
                table: "UserScores",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserScores_UserScoreModelUserScoreID",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserScores");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
