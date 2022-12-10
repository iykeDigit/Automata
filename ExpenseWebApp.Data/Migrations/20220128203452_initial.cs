using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseWebApp.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseAdvance_ExpenseStatus_ExpenseStatusId",
                table: "ExpenseAdvance");

            migrationBuilder.AlterColumn<string>(
                name: "ExpenseStatusId",
                table: "ExpenseAdvance",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseAdvance_ExpenseStatus_ExpenseStatusId",
                table: "ExpenseAdvance",
                column: "ExpenseStatusId",
                principalTable: "ExpenseStatus",
                principalColumn: "ExpenseStatusId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseAdvance_ExpenseStatus_ExpenseStatusId",
                table: "ExpenseAdvance");

            migrationBuilder.AlterColumn<string>(
                name: "ExpenseStatusId",
                table: "ExpenseAdvance",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseAdvance_ExpenseStatus_ExpenseStatusId",
                table: "ExpenseAdvance",
                column: "ExpenseStatusId",
                principalTable: "ExpenseStatus",
                principalColumn: "ExpenseStatusId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
