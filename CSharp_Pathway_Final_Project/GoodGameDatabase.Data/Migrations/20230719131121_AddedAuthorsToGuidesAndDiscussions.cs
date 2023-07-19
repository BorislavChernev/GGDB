using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodGameDatabase.Data.Migrations
{
    public partial class AddedAuthorsToGuidesAndDiscussions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discussions_Creators_CreatorId",
                table: "Discussions");

            migrationBuilder.DropForeignKey(
                name: "FK_Guides_Creators_CreatorId",
                table: "Guides");

            migrationBuilder.DropIndex(
                name: "IX_Guides_CreatorId",
                table: "Guides");

            migrationBuilder.DropIndex(
                name: "IX_Discussions_CreatorId",
                table: "Discussions");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Discussions");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "Guides",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "Discussions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Guides_AuthorId",
                table: "Guides",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Discussions_AuthorId",
                table: "Discussions",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discussions_AspNetUsers_AuthorId",
                table: "Discussions",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Guides_AspNetUsers_AuthorId",
                table: "Guides",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discussions_AspNetUsers_AuthorId",
                table: "Discussions");

            migrationBuilder.DropForeignKey(
                name: "FK_Guides_AspNetUsers_AuthorId",
                table: "Guides");

            migrationBuilder.DropIndex(
                name: "IX_Guides_AuthorId",
                table: "Guides");

            migrationBuilder.DropIndex(
                name: "IX_Discussions_AuthorId",
                table: "Discussions");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Discussions");

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Guides",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Discussions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Guides_CreatorId",
                table: "Guides",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Discussions_CreatorId",
                table: "Discussions",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discussions_Creators_CreatorId",
                table: "Discussions",
                column: "CreatorId",
                principalTable: "Creators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Guides_Creators_CreatorId",
                table: "Guides",
                column: "CreatorId",
                principalTable: "Creators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
