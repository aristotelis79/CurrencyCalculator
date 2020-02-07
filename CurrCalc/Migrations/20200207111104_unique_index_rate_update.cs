using Microsoft.EntityFrameworkCore.Migrations;

namespace CurrCalc.Migrations
{
    public partial class unique_index_rate_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_Source_Target",
                table: "CurrencyExchangeRate");

            migrationBuilder.CreateIndex(
                name: "idx_Source_Target",
                table: "CurrencyExchangeRate",
                columns: new[] { "SourceId", "TargetId", "From", "To" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_Source_Target",
                table: "CurrencyExchangeRate");

            migrationBuilder.CreateIndex(
                name: "idx_Source_Target",
                table: "CurrencyExchangeRate",
                columns: new[] { "SourceId", "TargetId" });
        }
    }
}
