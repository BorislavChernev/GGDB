using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodGameDatabase.Data.Migrations
{
    public partial class DiscussionsEntityFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Discussions_DiscussionId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Discussions_AspNetUsers_AuthorId",
                table: "Discussions");

            migrationBuilder.DropIndex(
                name: "IX_IdentityUserDiscussions_DiscussionId",
                table: "IdentityUserDiscussions");

            migrationBuilder.DropIndex(
                name: "IX_Discussions_AuthorId",
                table: "Discussions");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DiscussionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Discussions");

            migrationBuilder.DropColumn(
                name: "DiscussionId",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserDiscussions_DiscussionId",
                table: "IdentityUserDiscussions",
                column: "DiscussionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_IdentityUserDiscussions_DiscussionId",
                table: "IdentityUserDiscussions");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "Discussions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "DiscussionId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserDiscussions_DiscussionId",
                table: "IdentityUserDiscussions",
                column: "DiscussionId");

            migrationBuilder.CreateIndex(
                name: "IX_Discussions_AuthorId",
                table: "Discussions",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DiscussionId",
                table: "AspNetUsers",
                column: "DiscussionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Discussions_DiscussionId",
                table: "AspNetUsers",
                column: "DiscussionId",
                principalTable: "Discussions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Discussions_AspNetUsers_AuthorId",
                table: "Discussions",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
