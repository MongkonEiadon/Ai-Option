using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AiOption.Infrastructure.Migrations
{
    public partial class AddAccountReadModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IqAccountDetails");

            migrationBuilder.CreateTable(
                name: "AccountReadModels",
                columns: table => new
                {
                    AggregateId = table.Column<string>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountReadModels", x => x.AggregateId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountReadModels");

            migrationBuilder.CreateTable(
                name: "IqAccountDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Avartar = table.Column<string>(nullable: true),
                    Balance = table.Column<long>(nullable: false),
                    BalanceId = table.Column<long>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    CurrencyChar = table.Column<string>(nullable: true),
                    LastSyned = table.Column<DateTimeOffset>(nullable: true),
                    UpdatedDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IqAccountDetails", x => x.Id);
                });
        }
    }
}
