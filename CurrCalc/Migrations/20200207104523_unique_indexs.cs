using Microsoft.EntityFrameworkCore.Migrations;

namespace CurrCalc.Migrations
{
    public partial class unique_indexs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_Source_Target",
                table: "CurrencyExchangeRate");

            migrationBuilder.DropIndex(
                name: "idx_Currency_IsoCode",
                table: "Currency");

            migrationBuilder.CreateIndex(
                name: "idx_Source_Target",
                table: "CurrencyExchangeRate",
                columns: new[] { "SourceId", "TargetId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_Currency_IsoCode",
                table: "Currency",
                column: "IsoCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_Source_Target",
                table: "CurrencyExchangeRate");

            migrationBuilder.DropIndex(
                name: "idx_Currency_IsoCode",
                table: "Currency");

            migrationBuilder.CreateIndex(
                name: "idx_Source_Target",
                table: "CurrencyExchangeRate",
                columns: new[] { "SourceId", "TargetId" });

            migrationBuilder.CreateIndex(
                name: "idx_Currency_IsoCode",
                table: "Currency",
                column: "IsoCode");
        }
    }
}
