using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class FinanceTypeTbl2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Finances_FinanceType_FinanceTypeId",
                table: "Finances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinanceType",
                table: "FinanceType");

            migrationBuilder.RenameTable(
                name: "FinanceType",
                newName: "FinanceTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinanceTypes",
                table: "FinanceTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Finances_FinanceTypes_FinanceTypeId",
                table: "Finances",
                column: "FinanceTypeId",
                principalTable: "FinanceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Finances_FinanceTypes_FinanceTypeId",
                table: "Finances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinanceTypes",
                table: "FinanceTypes");

            migrationBuilder.RenameTable(
                name: "FinanceTypes",
                newName: "FinanceType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinanceType",
                table: "FinanceType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Finances_FinanceType_FinanceTypeId",
                table: "Finances",
                column: "FinanceTypeId",
                principalTable: "FinanceType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
