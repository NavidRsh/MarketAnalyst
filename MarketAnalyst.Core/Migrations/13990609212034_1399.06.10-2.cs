using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketAnalyst.Core.Migrations
{
    public partial class _139906102 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "EPS",
                table: "Stocks",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PE",
                table: "Stocks",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "EPS",
                table: "StockGroups",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PE",
                table: "StockGroups",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EPS",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "PE",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "EPS",
                table: "StockGroups");

            migrationBuilder.DropColumn(
                name: "PE",
                table: "StockGroups");
        }
    }
}
