using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketAnalyst.InfraStructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 300, nullable: true),
                    StockGroupId = table.Column<int>(nullable: false),
                    SupervisionLevel = table.Column<int>(nullable: false),
                    StocksCount = table.Column<int>(nullable: false),
                    BaseVolume = table.Column<int>(nullable: false),
                    FloatingStock = table.Column<double>(nullable: false),
                    AverageMonthVolume = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stocks_StockGroups_StockGroupId",
                        column: x => x.StockGroupId,
                        principalTable: "StockGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StocksDailyPrices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StockId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    InitialPrice = table.Column<double>(nullable: false),
                    LastPrice = table.Column<double>(nullable: false),
                    FinalPrice = table.Column<double>(nullable: false),
                    LowestPrice = table.Column<double>(nullable: false),
                    HighestPrice = table.Column<double>(nullable: false),
                    DealsCount = table.Column<int>(nullable: false),
                    DealsVolume = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StocksDailyPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StocksDailyPrices_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_StockGroupId",
                table: "Stocks",
                column: "StockGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StocksDailyPrices_StockId",
                table: "StocksDailyPrices",
                column: "StockId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StocksDailyPrices");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "StockGroups");
        }
    }
}
