using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iqoption.data.Migrations
{
    public partial class AddTraderRules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountType",
                table: "IqOptionAccount",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TraderRules",
                columns: table => new
                {
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Id = table.Column<Guid>(nullable: false),
                    IqAccountUserId = table.Column<int>(nullable: false),
                    Multiplier = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraderRules", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TraderRules");

            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "IqOptionAccount");
        }
    }
}
