using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodGameDatabase.Data.Migrations
{
    public partial class UpdateIdentityUserGuide : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Guides_GuideId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Guides_AspNetUsers_AuthorId",
                table: "Guides");

            migrationBuilder.DropIndex(
                name: "IX_IdentityUserGuides_GuideId",
                table: "IdentityUserGuides");

            migrationBuilder.DropIndex(
                name: "IX_Guides_AuthorId",
                table: "Guides");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GuideId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "GuideId",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserGuides_GuideId",
                table: "IdentityUserGuides",
                column: "GuideId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_IdentityUserGuides_GuideId",
                table: "IdentityUserGuides");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "Guides",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "GuideId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserGuides_GuideId",
                table: "IdentityUserGuides",
                column: "GuideId");

            migrationBuilder.CreateIndex(
                name: "IX_Guides_AuthorId",
                table: "Guides",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GuideId",
                table: "AspNetUsers",
                column: "GuideId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Guides_GuideId",
                table: "AspNetUsers",
                column: "GuideId",
                principalTable: "Guides",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Guides_AspNetUsers_AuthorId",
                table: "Guides",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
