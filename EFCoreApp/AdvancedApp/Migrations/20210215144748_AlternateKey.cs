using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvancedApp.Migrations
{
    public partial class AlternateKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_SSN",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "SSN",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Employees_SSN",
                table: "Employees",
                column: "SSN");

            migrationBuilder.CreateTable(
                name: "SecondaryIdentity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InActiveUse = table.Column<bool>(type: "bit", nullable: false),
                    PrimarySSN = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecondaryIdentity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecondaryIdentity_Employees_PrimarySSN",
                        column: x => x.PrimarySSN,
                        principalTable: "Employees",
                        principalColumn: "SSN",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SecondaryIdentity_PrimarySSN",
                table: "SecondaryIdentity",
                column: "PrimarySSN",
                unique: true,
                filter: "[PrimarySSN] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SecondaryIdentity");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Employees_SSN",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "SSN",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SSN",
                table: "Employees",
                column: "SSN",
                unique: true,
                filter: "[SSN] IS NOT NULL");
        }
    }
}
