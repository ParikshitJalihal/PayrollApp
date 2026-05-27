using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCM.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FKToEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EmployeeName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CandidateId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CandidateId",
                table: "Employees",
                column: "CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Candidates_CandidateId",
                table: "Employees",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "CandidateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Candidates_CandidateId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_CandidateId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
