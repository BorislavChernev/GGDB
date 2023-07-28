using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodGameDatabase.Data.Migrations
{
    public partial class AllDiscussionViewModelCreatedAndSeeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Discussions",
                columns: new[] { "Id", "DatePosted", "Description", "Topic", "pinned" },
                values: new object[] { 1, new DateTime(2023, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "This discussion is about cheating in singleplayer games", "Is Cheating in games bad", true });

            migrationBuilder.InsertData(
                table: "Discussions",
                columns: new[] { "Id", "DatePosted", "Description", "Topic", "pinned" },
                values: new object[] { 2, new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This discussion is about surviving in The Forest", "How to survive in The Forest", false });

            migrationBuilder.InsertData(
                table: "Discussions",
                columns: new[] { "Id", "DatePosted", "Description", "Topic", "pinned" },
                values: new object[] { 3, new DateTime(2023, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "This discussion is about dust storms in Astroneer", "Astroneer dust storms", false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
