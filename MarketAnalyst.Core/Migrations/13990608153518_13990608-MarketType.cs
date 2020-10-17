using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketAnalyst.Core.Migrations
{
    public partial class _13990608MarketType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MarketType",
                table: "Stocks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UniqueCode",
                table: "Stocks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarketType",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "UniqueCode",
                table: "Stocks");
        }
    }
}
