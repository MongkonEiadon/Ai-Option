using Microsoft.EntityFrameworkCore.Migrations;

namespace AiOption.Infrastructure.ReadStores.Migrations
{
    public partial class AddVersionReadModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                "Version",
                "IqAccounts",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                "Version",
                "Customers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Version",
                "IqAccounts");

            migrationBuilder.DropColumn(
                "Version",
                "Customers");
        }
    }
}