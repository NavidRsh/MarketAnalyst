using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketAnalyst.Core.Migrations
{
    public partial class _139907261 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuyingPowers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StockId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    IsHourly = table.Column<bool>(nullable: false),
                    StartTime = table.Column<TimeSpan>(nullable: false),
                    EndTime = table.Column<TimeSpan>(nullable: false),
                    TotalBuyPersonCount = table.Column<int>(nullable: false),
                    TotalBuyLegalCount = table.Column<int>(nullable: false),
                    TotalBuyPersonVolume = table.Column<int>(nullable: false),
                    TotalBuyLegalVolume = table.Column<int>(nullable: false),
                    TotalSellPersonCount = table.Column<int>(nullable: false),
                    TotalSellLegalCount = table.Column<int>(nullable: false),
                    TotalSellPersonVolume = table.Column<int>(nullable: false),
                    TotalSellLegalVolume = table.Column<int>(nullable: false),
                    TotalPersonBuyingPower = table.Column<double>(nullable: false),
                    TotalLegalBuyingPower = table.Column<double>(nullable: false),
                    BuyPersonCount = table.Column<int>(nullable: false),
                    BuyLegalCount = table.Column<int>(nullable: false),
                    BuyPersonVolume = table.Column<int>(nullable: false),
                    BuyLegalVolume = table.Column<int>(nullable: false),
                    SellPersonCount = table.Column<int>(nullable: false),
                    SellLegalCount = table.Column<int>(nullable: false),
                    SellPersonVolume = table.Column<int>(nullable: false),
                    SellLegalVolume = table.Column<int>(nullable: false),
                    LastPrice = table.Column<double>(nullable: false),
                    FinalPrice = table.Column<double>(nullable: false),
                    PersonBuyingPower = table.Column<double>(nullable: false),
                    LegalBuyingPower = table.Column<double>(nullable: false),
                    RegisterDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyingPowers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuyingPowers_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuyingPowers_StockId",
                table: "BuyingPowers",
                column: "StockId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuyingPowers");
        }
    }
}
