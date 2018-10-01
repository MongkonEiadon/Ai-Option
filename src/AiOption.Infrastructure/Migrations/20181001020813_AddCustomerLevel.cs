using Microsoft.EntityFrameworkCore.Migrations;

namespace AiOption.Infrastructure.Migrations
{
    public partial class AddCustomerLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "CustomerReadModel",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "CustomerReadModel");
        }
    }
}
