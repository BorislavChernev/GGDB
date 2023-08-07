using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodGameDatabase.Data.Migrations
{
    public partial class GuideChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Discussions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Discussions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Discussions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Guides");

            migrationBuilder.UpdateData(
                table: "Guides",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "How to stay alive");

            migrationBuilder.UpdateData(
                table: "Guides",
                keyColumn: "Id",
                keyValue: 3,
                column: "Title",
                value: "What car do you need NSFW Heat");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Guides",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Guides",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Discussions",
                columns: new[] { "Id", "CreatorId", "DatePosted", "Description", "Topic", "pinned" },
                values: new object[,]
                {
                    { 1, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "This discussion is about cheating in singleplayer games", "Is Cheating in games bad", true },
                    { 2, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This discussion is about surviving in The Forest", "How to survive in The Forest", false },
                    { 3, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "This discussion is about dust storms in Astroneer", "Astroneer dust storms", false }
                });

            migrationBuilder.UpdateData(
                table: "Guides",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Rating", "Title" },
                values: new object[] { "Do not be afraid of canibals", 95, "How to stay alive in The Forest" });

            migrationBuilder.UpdateData(
                table: "Guides",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Rating" },
                values: new object[] { "Play by the rules", 89 });

            migrationBuilder.UpdateData(
                table: "Guides",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Rating", "Title" },
                values: new object[] { "You need to buy the last car to beat the boss", 100, "What car do you need to outrace the final boss in NSFW Heat" });
        }
    }
}
