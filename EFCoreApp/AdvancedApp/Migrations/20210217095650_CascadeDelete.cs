using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvancedApp.Migrations
{
    public partial class CascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SecondaryIdentity_Employees_PrimarySSN_PrimaryFirstName_PrimaryFamilyName",
                table: "SecondaryIdentity");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 2, 16, 13, 47, 47, 795, DateTimeKind.Local).AddTicks(4283));

            migrationBuilder.AddForeignKey(
                name: "FK_SecondaryIdentity_Employees_PrimarySSN_PrimaryFirstName_PrimaryFamilyName",
                table: "SecondaryIdentity",
                columns: new[] { "PrimarySSN", "PrimaryFirstName", "PrimaryFamilyName" },
                principalTable: "Employees",
                principalColumns: new[] { "SSN", "FirstName", "FamilyName" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SecondaryIdentity_Employees_PrimarySSN_PrimaryFirstName_PrimaryFamilyName",
                table: "SecondaryIdentity");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 2, 16, 13, 47, 47, 795, DateTimeKind.Local).AddTicks(4283),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_SecondaryIdentity_Employees_PrimarySSN_PrimaryFirstName_PrimaryFamilyName",
                table: "SecondaryIdentity",
                columns: new[] { "PrimarySSN", "PrimaryFirstName", "PrimaryFamilyName" },
                principalTable: "Employees",
                principalColumns: new[] { "SSN", "FirstName", "FamilyName" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
