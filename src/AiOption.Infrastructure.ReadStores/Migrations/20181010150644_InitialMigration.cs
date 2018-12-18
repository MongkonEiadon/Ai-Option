using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AiOption.Infrastructure.ReadStores.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Customers",
                table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    InvitationCode = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: true),
                    LastLogin = table.Column<DateTimeOffset>(nullable: false),
                    Token = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Customers", x => x.Id); });

            migrationBuilder.CreateTable(
                "EventEntity",
                table => new
                {
                    GlobalSequenceNumber = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    BatchId = table.Column<Guid>(nullable: false),
                    AggregateName = table.Column<string>(nullable: true),
                    AggregateId = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    Metadata = table.Column<string>(nullable: true),
                    AggregateSequenceNumber = table.Column<int>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_EventEntity", x => x.GlobalSequenceNumber); });

            migrationBuilder.CreateTable(
                "IqAccounts",
                table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    IqOptionToken = table.Column<string>(nullable: true),
                    TokenUpdatedDate = table.Column<DateTimeOffset>(nullable: false),
                    CustomerId = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_IqAccounts", x => x.Id); });

            migrationBuilder.CreateTable(
                "SnapshotEntity",
                table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    AggregateId = table.Column<string>(nullable: true),
                    AggregateName = table.Column<string>(nullable: true),
                    AggregateSequenceNumber = table.Column<int>(nullable: false),
                    Data = table.Column<string>(nullable: true),
                    Metadata = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_SnapshotEntity", x => x.Id); });

            migrationBuilder.CreateIndex(
                "IX_EventEntity_AggregateId_AggregateSequenceNumber",
                "EventEntity",
                new[] {"AggregateId", "AggregateSequenceNumber"},
                unique: true,
                filter: "[AggregateId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                "IX_SnapshotEntity_AggregateName_AggregateId_AggregateSequenceNumber",
                "SnapshotEntity",
                new[] {"AggregateName", "AggregateId", "AggregateSequenceNumber"},
                unique: true,
                filter: "[AggregateName] IS NOT NULL AND [AggregateId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Customers");

            migrationBuilder.DropTable(
                "EventEntity");

            migrationBuilder.DropTable(
                "IqAccounts");

            migrationBuilder.DropTable(
                "SnapshotEntity");
        }
    }
}