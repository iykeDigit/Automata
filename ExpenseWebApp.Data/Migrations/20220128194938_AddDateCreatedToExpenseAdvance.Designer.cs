﻿// <auto-generated />
using System;
using ExpenseWebApp.Data.ContextClass;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExpenseWebApp.Data.Migrations
{
    [DbContext(typeof(ExpenseDbContext))]
    [Migration("20220128194938_AddDateCreatedToExpenseAdvance")]
    partial class AddDateCreatedToExpenseAdvance
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ExpenseWebApp.Models.AdvanceRetirement", b =>
                {
                    b.Property<string>("AdvanceRetirementId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AdvanceFormId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AdvanceRetirementFormNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApproverNote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Attachments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("ExpenseFormId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ExpenseStatusId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PaidBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaidFromId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("RetiredAmountDiff")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AdvanceRetirementId");

                    b.HasIndex("AdvanceFormId");

                    b.HasIndex("ExpenseFormId");

                    b.HasIndex("ExpenseStatusId");

                    b.HasIndex("PaidFromId");

                    b.ToTable("AdvanceRetirements");
                });

            modelBuilder.Entity("ExpenseWebApp.Models.CompanyFormData", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CACNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CashAdvanceFormCount")
                        .HasColumnType("int");

                    b.Property<int>("CashAdvanceRetirementFormCount")
                        .HasColumnType("int");

                    b.Property<int>("ExpenseFormCount")
                        .HasColumnType("int");

                    b.Property<string>("TransactionDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CACNumber")
                        .IsUnique();

                    b.ToTable("CompanyFormData");
                });

            modelBuilder.Entity("ExpenseWebApp.Models.ExpenseAccount", b =>
                {
                    b.Property<string>("ExpenseAccountId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("ExpenseAccountName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExpenseAccountNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExpenseAccountId");

                    b.ToTable("ExpenseAccounts");
                });

            modelBuilder.Entity("ExpenseWebApp.Models.ExpenseAdvance", b =>
                {
                    b.Property<string>("AdvanceFormId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("AdvanceAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("AdvanceDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("AdvanceDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdvanceFormNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdvanceNote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdvancePurpose")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApprovedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApproverNote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Attachments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("DisbursedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DisbursementDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExpenseStatusId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PaidFromId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AdvanceFormId");

                    b.HasIndex("ExpenseStatusId");

                    b.HasIndex("PaidFromId");

                    b.ToTable("ExpenseAdvance");
                });

            modelBuilder.Entity("ExpenseWebApp.Models.ExpenseCategory", b =>
                {
                    b.Property<string>("ExpenseCategoryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ExpenseCategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExpenseCategoryId");

                    b.ToTable("ExpenseCategories");
                });

            modelBuilder.Entity("ExpenseWebApp.Models.ExpenseForm", b =>
                {
                    b.Property<string>("ExpenseFormId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AdvanceFormId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApprovedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApproverNote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExpenseFormNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExpenseStatusId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FundedAccount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaidBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ReimburseableAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("ReimbursementDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ExpenseFormId");

                    b.HasIndex("AdvanceFormId");

                    b.HasIndex("ExpenseStatusId");

                    b.ToTable("ExpenseForms");
                });

            modelBuilder.Entity("ExpenseWebApp.Models.ExpenseFormDetails", b =>
                {
                    b.Property<string>("ExpenseFormDetailsId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Attachments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ExpenseAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ExpenseCategoryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ExpenseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExpenseFormId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ExpenseNote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PaidByCompany")
                        .HasColumnType("bit");

                    b.HasKey("ExpenseFormDetailsId");

                    b.HasIndex("ExpenseCategoryId");

                    b.HasIndex("ExpenseFormId");

                    b.ToTable("ExpenseFormDetails");
                });

            modelBuilder.Entity("ExpenseWebApp.Models.ExpenseStatus", b =>
                {
                    b.Property<string>("ExpenseStatusId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExpenseStatusId");

                    b.ToTable("ExpenseStatus");
                });

            modelBuilder.Entity("ExpenseWebApp.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("ExpenseFormNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FormNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FormStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<DateTime>("TimeStaamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("ExpenseWebApp.Models.PaidFrom", b =>
                {
                    b.Property<string>("PaidFromId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PaidFromName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaidFromId");

                    b.ToTable("PaidFrom");
                });

            modelBuilder.Entity("ExpenseWebApp.Models.AdvanceRetirement", b =>
                {
                    b.HasOne("ExpenseWebApp.Models.ExpenseAdvance", "AdvanceForm")
                        .WithMany("AdvanceRetirement")
                        .HasForeignKey("AdvanceFormId");

                    b.HasOne("ExpenseWebApp.Models.ExpenseForm", "ExpenseForm")
                        .WithMany("AdvanceRetirement")
                        .HasForeignKey("ExpenseFormId");

                    b.HasOne("ExpenseWebApp.Models.ExpenseStatus", "ExpenseStatus")
                        .WithMany()
                        .HasForeignKey("ExpenseStatusId");

                    b.HasOne("ExpenseWebApp.Models.PaidFrom", "PaidFrom")
                        .WithMany("AdvanceRetirement")
                        .HasForeignKey("PaidFromId");

                    b.Navigation("AdvanceForm");

                    b.Navigation("ExpenseForm");

                    b.Navigation("ExpenseStatus");

                    b.Navigation("PaidFrom");
                });

            modelBuilder.Entity("ExpenseWebApp.Models.ExpenseAdvance", b =>
                {
                    b.HasOne("ExpenseWebApp.Models.ExpenseStatus", "ExpenseStatus")
                        .WithMany()
                        .HasForeignKey("ExpenseStatusId");

                    b.HasOne("ExpenseWebApp.Models.PaidFrom", "PaidFrom")
                        .WithMany("ExpenseAdvance")
                        .HasForeignKey("PaidFromId");

                    b.Navigation("ExpenseStatus");

                    b.Navigation("PaidFrom");
                });

            modelBuilder.Entity("ExpenseWebApp.Models.ExpenseForm", b =>
                {
                    b.HasOne("ExpenseWebApp.Models.ExpenseAdvance", "AdvanceForm")
                        .WithMany()
                        .HasForeignKey("AdvanceFormId");

                    b.HasOne("ExpenseWebApp.Models.ExpenseStatus", "ExpenseStatus")
                        .WithMany()
                        .HasForeignKey("ExpenseStatusId");

                    b.Navigation("AdvanceForm");

                    b.Navigation("ExpenseStatus");
                });

            modelBuilder.Entity("ExpenseWebApp.Models.ExpenseFormDetails", b =>
                {
                    b.HasOne("ExpenseWebApp.Models.ExpenseCategory", "ExpenseCategory")
                        .WithMany("ExpenseFormDetails")
                        .HasForeignKey("ExpenseCategoryId");

                    b.HasOne("ExpenseWebApp.Models.ExpenseForm", "ExpenseForm")
                        .WithMany("ExpenseFormDetails")
                        .HasForeignKey("ExpenseFormId");

                    b.Navigation("ExpenseCategory");

                    b.Navigation("ExpenseForm");
                });

            modelBuilder.Entity("ExpenseWebApp.Models.ExpenseAdvance", b =>
                {
                    b.Navigation("AdvanceRetirement");
                });

            modelBuilder.Entity("ExpenseWebApp.Models.ExpenseCategory", b =>
                {
                    b.Navigation("ExpenseFormDetails");
                });

            modelBuilder.Entity("ExpenseWebApp.Models.ExpenseForm", b =>
                {
                    b.Navigation("AdvanceRetirement");

                    b.Navigation("ExpenseFormDetails");
                });

            modelBuilder.Entity("ExpenseWebApp.Models.PaidFrom", b =>
                {
                    b.Navigation("AdvanceRetirement");

                    b.Navigation("ExpenseAdvance");
                });
#pragma warning restore 612, 618
        }
    }
}
