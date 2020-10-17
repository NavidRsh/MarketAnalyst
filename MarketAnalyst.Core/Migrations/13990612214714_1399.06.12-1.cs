using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketAnalyst.Core.Migrations
{
    public partial class _139906121 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "StocksDailyPrices",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<double>(
                name: "DealsValue",
                table: "StocksDailyPrices",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "FinalPriceChange",
                table: "StocksDailyPrices",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "FinalPriceChangePercent",
                table: "StocksDailyPrices",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LastPriceChange",
                table: "StocksDailyPrices",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LastPriceChangePercent",
                table: "StocksDailyPrices",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PreviousDayPrice",
                table: "StocksDailyPrices",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "StockGroups",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "StockGroups",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DealsValue",
                table: "StocksDailyPrices");

            migrationBuilder.DropColumn(
                name: "FinalPriceChange",
                table: "StocksDailyPrices");

            migrationBuilder.DropColumn(
                name: "FinalPriceChangePercent",
                table: "StocksDailyPrices");

            migrationBuilder.DropColumn(
                name: "LastPriceChange",
                table: "StocksDailyPrices");

            migrationBuilder.DropColumn(
                name: "LastPriceChangePercent",
                table: "StocksDailyPrices");

            migrationBuilder.DropColumn(
                name: "PreviousDayPrice",
                table: "StocksDailyPrices");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "StocksDailyPrices",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "StockGroups",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "StockGroups",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 300,
                oldNullable: true);
        }
    }
}
