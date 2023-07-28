using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodGameDatabase.Data.Migrations
{
    public partial class GuideDataModelChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Guides",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Guides",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Guides",
                columns: new[] { "Id", "Category", "GameId", "Language", "Rating", "Title" },
                values: new object[] { 1, 0, 1, 0, 95, "How to stay alive in The Forest" });

            migrationBuilder.InsertData(
                table: "Guides",
                columns: new[] { "Id", "Category", "GameId", "Language", "Rating", "Title" },
                values: new object[] { 2, 2, 3, 0, 89, "How to complete Astroneer" });

            migrationBuilder.InsertData(
                table: "Guides",
                columns: new[] { "Id", "Category", "GameId", "Language", "Rating", "Title" },
                values: new object[] { 3, 2, 2, 0, 100, "What car do you need to outrace the final boss in NSFW Heat" });

            migrationBuilder.CreateIndex(
                name: "IX_Guides_GameId",
                table: "Guides",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Guides_Games_GameId",
                table: "Guides",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guides_Games_GameId",
                table: "Guides");

            migrationBuilder.DropIndex(
                name: "IX_Guides_GameId",
                table: "Guides");

            migrationBuilder.DeleteData(
                table: "Guides",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Guides",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Guides",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Guides");

            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "Guides",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
