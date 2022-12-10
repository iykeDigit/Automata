using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseWebApp.Data.Migrations
{
    public partial class initialM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpenseAccounts",
                columns: table => new
                {
                    ExpenseAccountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExpenseAccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpenseAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseAccounts", x => x.ExpenseAccountId);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseCategories",
                columns: table => new
                {
                    ExpenseCategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExpenseCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseCategories", x => x.ExpenseCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseStatus",
                columns: table => new
                {
                    ExpenseStatusId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseStatus", x => x.ExpenseStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TimeStaamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpenseFormNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaidFrom",
                columns: table => new
                {
                    PaidFromId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PaidFromName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaidFrom", x => x.PaidFromId);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseAdvance",
                columns: table => new
                {
                    AdvanceFormId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdvanceFormNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdvanceDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdvanceAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdvancePurpose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdvanceNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdvanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApproverNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisbursementDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DisbursedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attachments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    PaidFromId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ExpenseStatusId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseAdvance", x => x.AdvanceFormId);
                    table.ForeignKey(
                        name: "FK_ExpenseAdvance_ExpenseStatus_ExpenseStatusId",
                        column: x => x.ExpenseStatusId,
                        principalTable: "ExpenseStatus",
                        principalColumn: "ExpenseStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpenseAdvance_PaidFrom_PaidFromId",
                        column: x => x.PaidFromId,
                        principalTable: "PaidFrom",
                        principalColumn: "PaidFromId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseForms",
                columns: table => new
                {
                    ExpenseFormId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ExpenseFormNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReimburseableAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReimbursementDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaidBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    FundedAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApproverNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpenseStatusId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AdvanceFormId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseForms", x => x.ExpenseFormId);
                    table.ForeignKey(
                        name: "FK_ExpenseForms_ExpenseAdvance_AdvanceFormId",
                        column: x => x.AdvanceFormId,
                        principalTable: "ExpenseAdvance",
                        principalColumn: "AdvanceFormId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpenseForms_ExpenseStatus_ExpenseStatusId",
                        column: x => x.ExpenseStatusId,
                        principalTable: "ExpenseStatus",
                        principalColumn: "ExpenseStatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdvanceRetirements",
                columns: table => new
                {
                    AdvanceRetirementId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdvanceRetirementFormNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RetiredAmountDiff = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    PaidBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApproverNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpenseStatusId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PaidFromId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ExpenseFormId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Attachments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdvanceFormId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvanceRetirements", x => x.AdvanceRetirementId);
                    table.ForeignKey(
                        name: "FK_AdvanceRetirements_ExpenseAdvance_AdvanceFormId",
                        column: x => x.AdvanceFormId,
                        principalTable: "ExpenseAdvance",
                        principalColumn: "AdvanceFormId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvanceRetirements_ExpenseForms_ExpenseFormId",
                        column: x => x.ExpenseFormId,
                        principalTable: "ExpenseForms",
                        principalColumn: "ExpenseFormId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvanceRetirements_ExpenseStatus_ExpenseStatusId",
                        column: x => x.ExpenseStatusId,
                        principalTable: "ExpenseStatus",
                        principalColumn: "ExpenseStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvanceRetirements_PaidFrom_PaidFromId",
                        column: x => x.PaidFromId,
                        principalTable: "PaidFrom",
                        principalColumn: "PaidFromId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseFormDetails",
                columns: table => new
                {
                    ExpenseFormDetailsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExpenseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpenseAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaidByCompany = table.Column<bool>(type: "bit", nullable: false),
                    ExpenseNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attachments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpenseFormId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ExpenseCategoryId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseFormDetails", x => x.ExpenseFormDetailsId);
                    table.ForeignKey(
                        name: "FK_ExpenseFormDetails_ExpenseCategories_ExpenseCategoryId",
                        column: x => x.ExpenseCategoryId,
                        principalTable: "ExpenseCategories",
                        principalColumn: "ExpenseCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpenseFormDetails_ExpenseForms_ExpenseFormId",
                        column: x => x.ExpenseFormId,
                        principalTable: "ExpenseForms",
                        principalColumn: "ExpenseFormId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceRetirements_AdvanceFormId",
                table: "AdvanceRetirements",
                column: "AdvanceFormId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceRetirements_ExpenseFormId",
                table: "AdvanceRetirements",
                column: "ExpenseFormId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceRetirements_ExpenseStatusId",
                table: "AdvanceRetirements",
                column: "ExpenseStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceRetirements_PaidFromId",
                table: "AdvanceRetirements",
                column: "PaidFromId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseAdvance_ExpenseStatusId",
                table: "ExpenseAdvance",
                column: "ExpenseStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseAdvance_PaidFromId",
                table: "ExpenseAdvance",
                column: "PaidFromId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseFormDetails_ExpenseCategoryId",
                table: "ExpenseFormDetails",
                column: "ExpenseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseFormDetails_ExpenseFormId",
                table: "ExpenseFormDetails",
                column: "ExpenseFormId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseForms_AdvanceFormId",
                table: "ExpenseForms",
                column: "AdvanceFormId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseForms_ExpenseStatusId",
                table: "ExpenseForms",
                column: "ExpenseStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvanceRetirements");

            migrationBuilder.DropTable(
                name: "ExpenseAccounts");

            migrationBuilder.DropTable(
                name: "ExpenseFormDetails");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "ExpenseCategories");

            migrationBuilder.DropTable(
                name: "ExpenseForms");

            migrationBuilder.DropTable(
                name: "ExpenseAdvance");

            migrationBuilder.DropTable(
                name: "ExpenseStatus");

            migrationBuilder.DropTable(
                name: "PaidFrom");
        }
    }
}
