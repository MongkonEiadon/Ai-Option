using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace iqoption.data.Migrations
{
    public partial class AddTraderAndHostMapped : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Follower",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    IqOptionUserDtoId = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Follower", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Follower_IqOptionUser_IqOptionUserDtoId",
                        column: x => x.IqOptionUserDtoId,
                        principalTable: "IqOptionUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trader",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    IqOptionUserDtoId = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trader_IqOptionUser_IqOptionUserDtoId",
                        column: x => x.IqOptionUserDtoId,
                        principalTable: "IqOptionUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TraderFollwers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    FollowerId = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    TraderId = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraderFollwers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TraderFollwers_Follower_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "Follower",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TraderFollwers_Trader_TraderId",
                        column: x => x.TraderId,
                        principalTable: "Trader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Follower_IqOptionUserDtoId",
                table: "Follower",
                column: "IqOptionUserDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_Trader_IqOptionUserDtoId",
                table: "Trader",
                column: "IqOptionUserDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_TraderFollwers_FollowerId",
                table: "TraderFollwers",
                column: "FollowerId");

            migrationBuilder.CreateIndex(
                name: "IX_TraderFollwers_TraderId",
                table: "TraderFollwers",
                column: "TraderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TraderFollwers");

            migrationBuilder.DropTable(
                name: "Follower");

            migrationBuilder.DropTable(
                name: "Trader");
        }
    }
}
