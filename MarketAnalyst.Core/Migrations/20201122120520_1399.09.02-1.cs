using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketAnalyst.Core.Migrations
{
    public partial class _139909021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "TotalSellPersonVolume",
                table: "BuyingPowers",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "TotalSellLegalVolume",
                table: "BuyingPowers",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "TotalBuyPersonVolume",
                table: "BuyingPowers",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "TotalBuyLegalVolume",
                table: "BuyingPowers",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "SellPersonVolume",
                table: "BuyingPowers",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "SellLegalVolume",
                table: "BuyingPowers",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "BuyPersonVolume",
                table: "BuyingPowers",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "BuyLegalVolume",
                table: "BuyingPowers",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TotalSellPersonVolume",
                table: "BuyingPowers",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "TotalSellLegalVolume",
                table: "BuyingPowers",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "TotalBuyPersonVolume",
                table: "BuyingPowers",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "TotalBuyLegalVolume",
                table: "BuyingPowers",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "SellPersonVolume",
                table: "BuyingPowers",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "SellLegalVolume",
                table: "BuyingPowers",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "BuyPersonVolume",
                table: "BuyingPowers",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "BuyLegalVolume",
                table: "BuyingPowers",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
