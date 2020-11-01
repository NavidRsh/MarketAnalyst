using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketAnalyst.Core.Migrations
{
    public partial class _139908101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "FinalPriceChangePercent",
                table: "BuyingPowers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "FirstPrice",
                table: "BuyingPowers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "HighestPrice",
                table: "BuyingPowers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LastPriceChangePercent",
                table: "BuyingPowers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LowestPrice",
                table: "BuyingPowers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PreviousDayPrice",
                table: "BuyingPowers",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalPriceChangePercent",
                table: "BuyingPowers");

            migrationBuilder.DropColumn(
                name: "FirstPrice",
                table: "BuyingPowers");

            migrationBuilder.DropColumn(
                name: "HighestPrice",
                table: "BuyingPowers");

            migrationBuilder.DropColumn(
                name: "LastPriceChangePercent",
                table: "BuyingPowers");

            migrationBuilder.DropColumn(
                name: "LowestPrice",
                table: "BuyingPowers");

            migrationBuilder.DropColumn(
                name: "PreviousDayPrice",
                table: "BuyingPowers");
        }
    }
}
