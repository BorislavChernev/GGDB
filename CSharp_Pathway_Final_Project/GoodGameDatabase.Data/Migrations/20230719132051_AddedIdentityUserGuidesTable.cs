using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodGameDatabase.Data.Migrations
{
    public partial class AddedIdentityUserGuidesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IdentityUserGuides",
                columns: table => new
                {
                    GuideId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserGuides", x => new { x.UserId, x.GuideId });
                    table.ForeignKey(
                        name: "FK_IdentityUserGuides_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_IdentityUserGuides_Guides_GuideId",
                        column: x => x.GuideId,
                        principalTable: "Guides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserGuides_GuideId",
                table: "IdentityUserGuides",
                column: "GuideId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityUserGuides");
        }
    }
}
