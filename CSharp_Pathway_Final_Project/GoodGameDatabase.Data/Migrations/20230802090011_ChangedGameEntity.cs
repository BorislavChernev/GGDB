using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodGameDatabase.Data.Migrations
{
    public partial class ChangedGameEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SupportsWindows",
                table: "Games",
                newName: "SupportsXbox");

            migrationBuilder.RenameColumn(
                name: "SupportsMacOs",
                table: "Games",
                newName: "SupportsPS");

            migrationBuilder.RenameColumn(
                name: "SupportsLinux",
                table: "Games",
                newName: "SupportsPC");

            migrationBuilder.AddColumn<bool>(
                name: "SupportsNintendo",
                table: "Games",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "SupportsPC", "SupportsPS" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "SupportsPC", "SupportsXbox" },
                values: new object[] { true, false });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "SupportsPC", "SupportsXbox" },
                values: new object[] { true, false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupportsNintendo",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "SupportsXbox",
                table: "Games",
                newName: "SupportsWindows");

            migrationBuilder.RenameColumn(
                name: "SupportsPS",
                table: "Games",
                newName: "SupportsMacOs");

            migrationBuilder.RenameColumn(
                name: "SupportsPC",
                table: "Games",
                newName: "SupportsLinux");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "SupportsLinux", "SupportsMacOs" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "SupportsLinux", "SupportsWindows" },
                values: new object[] { false, true });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "SupportsLinux", "SupportsWindows" },
                values: new object[] { false, true });
        }
    }
}
