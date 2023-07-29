using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodGameDatabase.Data.Migrations
{
    public partial class EditedGamePartial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://cdn.akamai.steamstatic.com/steam/apps/242760/capsule_616x353.jpg?t=1666811027");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://cdn.cloudflare.steamstatic.com/steam/apps/1222680/capsule_616x353.jpg?t=1690398297");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://cdn.akamai.steamstatic.com/steam/apps/361420/capsule_616x353.jpg?t=1689355883");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://cdn.discordapp.com/attachments/856238725162729525/1134206890872688700/the-forest-banner.jpg");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://cdn.discordapp.com/attachments/856238725162729525/1134208308429991996/6552b32dea82b16326bbc34931dae2f4.png");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://cdn.discordapp.com/attachments/856238725162729525/1134209239955538071/capsule_467x181.jpg");
        }
    }
}
