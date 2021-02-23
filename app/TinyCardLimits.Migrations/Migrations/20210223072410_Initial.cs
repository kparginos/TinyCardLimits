using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyCardLimits.Migrations.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    CardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvailableBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.CardId);
                });

            migrationBuilder.CreateTable(
                name: "Limit",
                columns: table => new
                {
                    CardLimitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    AggrAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ApplyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CardId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Limit", x => x.CardLimitId);
                    table.ForeignKey(
                        name: "FK_Limit_Card_CardId",
                        column: x => x.CardId,
                        principalTable: "Card",
                        principalColumn: "CardId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Limit_CardId",
                table: "Limit",
                column: "CardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Limit");

            migrationBuilder.DropTable(
                name: "Card");
        }
    }
}
