using Microsoft.EntityFrameworkCore.Migrations;

namespace AiOption.Infrastructure.ReadStores.Migrations
{
    public partial class AddVersionReadModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "IqAccounts",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "Customers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "IqAccounts");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Customers");
        }
    }
}
