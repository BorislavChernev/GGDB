using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodGameDatabase.Data.Migrations
{
    public partial class AddedDiscussionParticipant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Creators_CreatorId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "IdentityUserDiscussions");

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "Games",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Discussions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "DiscussionParticipants",
                columns: table => new
                {
                    DiscussionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscussionParticipants", x => new { x.UserId, x.DiscussionId });
                    table.ForeignKey(
                        name: "FK_DiscussionParticipants_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiscussionParticipants_Discussions_DiscussionId",
                        column: x => x.DiscussionId,
                        principalTable: "Discussions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatorId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatorId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatorId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Discussions_CreatorId",
                table: "Discussions",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionParticipants_DiscussionId",
                table: "DiscussionParticipants",
                column: "DiscussionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discussions_AspNetUsers_CreatorId",
                table: "Discussions",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Creators_CreatorId",
                table: "Games",
                column: "CreatorId",
                principalTable: "Creators",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discussions_AspNetUsers_CreatorId",
                table: "Discussions");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Creators_CreatorId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "DiscussionParticipants");

            migrationBuilder.DropIndex(
                name: "IX_Discussions_CreatorId",
                table: "Discussions");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Discussions");

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "IdentityUserDiscussions",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiscussionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserDiscussions", x => new { x.UserId, x.DiscussionId });
                    table.ForeignKey(
                        name: "FK_IdentityUserDiscussions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdentityUserDiscussions_Discussions_DiscussionId",
                        column: x => x.DiscussionId,
                        principalTable: "Discussions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatorId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatorId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatorId",
                value: 3);

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserDiscussions_DiscussionId",
                table: "IdentityUserDiscussions",
                column: "DiscussionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Creators_CreatorId",
                table: "Games",
                column: "CreatorId",
                principalTable: "Creators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
