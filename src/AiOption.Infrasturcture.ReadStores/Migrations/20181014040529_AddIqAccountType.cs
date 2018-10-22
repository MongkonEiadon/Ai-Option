using Microsoft.EntityFrameworkCore.Migrations;

namespace AiOption.Infrastructure.ReadStores.Migrations
{
    public partial class AddIqAccountType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "IqAccounts",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "IqAccounts");
        }
    }
}
