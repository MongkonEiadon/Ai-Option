using Microsoft.EntityFrameworkCore.Migrations;

namespace iqoption.data.Migrations {
    public partial class AddInviationCodeToUser : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AddColumn<string>(
                name: "InviationCode",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropColumn(
                name: "InviationCode",
                table: "AspNetUsers");
        }
    }
}