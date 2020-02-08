using Microsoft.EntityFrameworkCore.Migrations;

namespace CurrCalc.Migrations
{
    public partial class lang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "idx_Source_Target",
                table: "CurrencyExchangeRate",
                newName: "idx_Source_Target_From_To");

            migrationBuilder.AlterColumn<string>(
                name: "IsoCode",
                table: "Currency",
                type: "char(3)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3);

            migrationBuilder.CreateTable(
                name: "LocalizedText",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Language = table.Column<string>(type: "char(2)", nullable: true),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizedText", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "idx_Lang_Key",
                table: "LocalizedText",
                columns: new[] { "Language", "Key" },
                unique: true,
                filter: "[Language] IS NOT NULL AND [Key] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalizedText");

            migrationBuilder.RenameIndex(
                name: "idx_Source_Target_From_To",
                table: "CurrencyExchangeRate",
                newName: "idx_Source_Target");

            migrationBuilder.AlterColumn<string>(
                name: "IsoCode",
                table: "Currency",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(3)");
        }
    }
}
