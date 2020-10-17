using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketAnalyst.Core.Migrations
{
    public partial class _139906131 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "DealsVolume",
                table: "StocksDailyPrices",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "DealsValue",
                table: "StocksDailyPrices",
                nullable: false,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DealsVolume",
                table: "StocksDailyPrices",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<double>(
                name: "DealsValue",
                table: "StocksDailyPrices",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
