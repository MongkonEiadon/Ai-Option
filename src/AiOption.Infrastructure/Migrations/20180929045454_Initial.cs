using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AiOption.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerReadModel",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    InvitationCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerReadModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventEntity",
                columns: table => new
                {
                    GlobalSequenceNumber = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BatchId = table.Column<Guid>(nullable: false),
                    AggregateName = table.Column<string>(nullable: true),
                    AggregateId = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    Metadata = table.Column<string>(nullable: true),
                    AggregateSequenceNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventEntity", x => x.GlobalSequenceNumber);
                });

            migrationBuilder.CreateTable(
                name: "SnapshotEntity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AggregateId = table.Column<string>(nullable: true),
                    AggregateName = table.Column<string>(nullable: true),
                    AggregateSequenceNumber = table.Column<int>(nullable: false),
                    Data = table.Column<string>(nullable: true),
                    Metadata = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnapshotEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IqAccountReadModel",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    CustomerReadModelAggregateId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IqAccountReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IqAccountReadModel_CustomerReadModel_CustomerReadModelAggregateId",
                        column: x => x.CustomerReadModelAggregateId,
                        principalTable: "CustomerReadModel",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventEntity_AggregateId_AggregateSequenceNumber",
                table: "EventEntity",
                columns: new[] { "AggregateId", "AggregateSequenceNumber" },
                unique: true,
                filter: "[AggregateId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IqAccountReadModel_CustomerReadModelAggregateId",
                table: "IqAccountReadModel",
                column: "CustomerReadModelAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_SnapshotEntity_AggregateName_AggregateId_AggregateSequenceNumber",
                table: "SnapshotEntity",
                columns: new[] { "AggregateName", "AggregateId", "AggregateSequenceNumber" },
                unique: true,
                filter: "[AggregateName] IS NOT NULL AND [AggregateId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventEntity");

            migrationBuilder.DropTable(
                name: "IqAccountReadModel");

            migrationBuilder.DropTable(
                name: "SnapshotEntity");

            migrationBuilder.DropTable(
                name: "CustomerReadModel");
        }
    }
}
