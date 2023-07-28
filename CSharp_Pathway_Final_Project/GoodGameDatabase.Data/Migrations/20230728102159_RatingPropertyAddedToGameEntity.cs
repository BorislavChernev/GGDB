using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodGameDatabase.Data.Migrations
{
    public partial class RatingPropertyAddedToGameEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Creators",
                columns: new[] { "Id", "DateEstablished", "Name" },
                values: new object[] { 1, new DateTime(2013, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Endnight Games Ltd" });

            migrationBuilder.InsertData(
                table: "Creators",
                columns: new[] { "Id", "DateEstablished", "Name" },
                values: new object[] { 2, new DateTime(2011, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ghost Games" });

            migrationBuilder.InsertData(
                table: "Creators",
                columns: new[] { "Id", "DateEstablished", "Name" },
                values: new object[] { 3, new DateTime(2014, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System Era Softworks" });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "AgeRestriction", "CreatorId", "Description", "ImageUrl", "Name", "Rating", "ReleaseDate", "Status", "SupportsLinux", "SupportsMacOs", "SupportsWindows", "Version" },
                values: new object[] { 1, 4, 1, "As the lone survivor of a passenger jet crash, you find yourself in a mysterious forest battling to stay alive", "https://cdn.discordapp.com/attachments/856238725162729525/1134206890872688700/the-forest-banner.jpg", "The Forest", 89, new DateTime(2014, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, false, false, true, "v1.12.0 - VR" });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "AgeRestriction", "CreatorId", "Description", "ImageUrl", "Name", "Rating", "ReleaseDate", "Status", "SupportsLinux", "SupportsMacOs", "SupportsWindows", "Version" },
                values: new object[] { 2, 3, 2, "A thrilling race experience pits you against a city’s rogue police force.", "https://cdn.discordapp.com/attachments/856238725162729525/1134208308429991996/6552b32dea82b16326bbc34931dae2f4.png", "Need for Speed™ Heat", 76, new DateTime(2019, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, false, false, true, "v1.0.0" });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "AgeRestriction", "CreatorId", "Description", "ImageUrl", "Name", "Rating", "ReleaseDate", "Status", "SupportsLinux", "SupportsMacOs", "SupportsWindows", "Version" },
                values: new object[] { 3, 1, 3, "Explore and reshape distant worlds!", "https://cdn.discordapp.com/attachments/856238725162729525/1134209239955538071/capsule_467x181.jpg", "Astroneer", 92, new DateTime(2016, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, false, false, true, "v1.2.6" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Creators",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Creators",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Creators",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Games");
        }
    }
}
