using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iqoption.data.Migrations
{
    public partial class AddSsidToAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ssid",
                table: "IqOptionAccount",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SsidUpdated",
                table: "IqOptionAccount",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ssid",
                table: "IqOptionAccount");

            migrationBuilder.DropColumn(
                name: "SsidUpdated",
                table: "IqOptionAccount");
        }
    }
}
