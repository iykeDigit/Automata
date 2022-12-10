using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseWebApp.Data.Migrations
{
    public partial class AddDateCreatedToExpenseAdvance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "ExpenseAdvance",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "CompanyFormData",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CACNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExpenseFormCount = table.Column<int>(type: "int", nullable: false),
                    CashAdvanceFormCount = table.Column<int>(type: "int", nullable: false),
                    CashAdvanceRetirementFormCount = table.Column<int>(type: "int", nullable: false),
                    TransactionDate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyFormData", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFormData_CACNumber",
                table: "CompanyFormData",
                column: "CACNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyFormData");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "ExpenseAdvance");
        }
    }
}
