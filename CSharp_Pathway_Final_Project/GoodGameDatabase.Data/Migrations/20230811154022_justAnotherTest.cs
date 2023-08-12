using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodGameDatabase.Data.Migrations
{
    public partial class justAnotherTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("2eea732f-0d67-4a0e-a462-19b311cfb353"), 0, "64591496-6319-40a4-a4bf-7c50acc4c24b", "admin@mail.com", true, false, null, "ADMIN@MAIL.COM", "ADMIN@MAIL.COM", "AQAAAAEAACcQAAAAELKOjvNdo+XXEzZR5bTuItshEgHgCd2WkzLRayeMAnpJOR7xpTu0uE7vybtQjgy5dA==", null, false, "", false, "admin@mail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("5924c262-0a23-4d4b-9101-fcf7a1ad0f6b"), 0, "2e01b192-32b0-480c-882d-ea36b38ee14f", "user2@mail.com", true, false, null, "USER2@MAIL.COM", "USER2@MAIL.COM", "AQAAAAEAACcQAAAAEGGyJ1QN9PRvZoyAH1BVrNxHU6WLJ2diTLFgIYtE7WnqneEIX8WWrEKCxHicOetoBQ==", null, false, "", false, "user2@mail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("cc0c5b30-afc5-4cd6-9ed9-b64e584afc89"), 0, "bf18bec2-a402-4bb1-a49c-ca98c6df6d64", "user1@mail.com", true, false, null, "USER1@MAIL.COM", "USER1@MAIL.COM", "AQAAAAEAACcQAAAAEC4rEKXePbgLg7rmtRLx58pIK+2od8oh6q0QHIjFE1yP5gEzITJGoKVv4bTKh+dSlw==", null, false, "", false, "user1@mail.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2eea732f-0d67-4a0e-a462-19b311cfb353"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5924c262-0a23-4d4b-9101-fcf7a1ad0f6b"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cc0c5b30-afc5-4cd6-9ed9-b64e584afc89"));
        }
    }
}
