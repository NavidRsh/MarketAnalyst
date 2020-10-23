using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketAnalyst.Core.Migrations
{
    public partial class _139907301 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AverageLegalBuy",
                table: "BuyingPowers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AveragePersonBuy",
                table: "BuyingPowers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalAverageLegalBuy",
                table: "BuyingPowers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalAveragePersonBuy",
                table: "BuyingPowers",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageLegalBuy",
                table: "BuyingPowers");

            migrationBuilder.DropColumn(
                name: "AveragePersonBuy",
                table: "BuyingPowers");

            migrationBuilder.DropColumn(
                name: "TotalAverageLegalBuy",
                table: "BuyingPowers");

            migrationBuilder.DropColumn(
                name: "TotalAveragePersonBuy",
                table: "BuyingPowers");
        }
    }
}
