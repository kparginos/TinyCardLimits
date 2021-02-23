using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyCardLimits.Migrations.Migrations
{
    public partial class Add_CardNumber_As_UniqueKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CardNumber",
                table: "Card",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Card_CardNumber",
                table: "Card",
                column: "CardNumber",
                unique: true,
                filter: "[CardNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Card_CardNumber",
                table: "Card");

            migrationBuilder.AlterColumn<string>(
                name: "CardNumber",
                table: "Card",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
