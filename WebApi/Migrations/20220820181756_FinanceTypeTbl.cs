using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class FinanceTypeTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Finances");

            migrationBuilder.AddColumn<int>(
                name: "FinanceTypeId",
                table: "Finances",
                type: "int",
                maxLength: 250,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FinanceType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Finances_FinanceTypeId",
                table: "Finances",
                column: "FinanceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Finances_FinanceType_FinanceTypeId",
                table: "Finances",
                column: "FinanceTypeId",
                principalTable: "FinanceType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Finances_FinanceType_FinanceTypeId",
                table: "Finances");

            migrationBuilder.DropTable(
                name: "FinanceType");

            migrationBuilder.DropIndex(
                name: "IX_Finances_FinanceTypeId",
                table: "Finances");

            migrationBuilder.DropColumn(
                name: "FinanceTypeId",
                table: "Finances");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Finances",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }
    }
}
