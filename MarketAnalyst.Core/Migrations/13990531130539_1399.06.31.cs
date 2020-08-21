using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketAnalyst.Core.Migrations
{
    public partial class _13990631 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Stocks",
                newName: "PersianSign");

            migrationBuilder.AddColumn<string>(
                name: "EnglishName",
                table: "Stocks",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnglishSign",
                table: "Stocks",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InfoUrl",
                table: "Stocks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersianName",
                table: "Stocks",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "StockGroups",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InfoUrl",
                table: "StockGroups",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnglishName",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "EnglishSign",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "InfoUrl",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "PersianName",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "StockGroups");

            migrationBuilder.DropColumn(
                name: "InfoUrl",
                table: "StockGroups");

            migrationBuilder.RenameColumn(
                name: "PersianSign",
                table: "Stocks",
                newName: "Name");
        }
    }
}
