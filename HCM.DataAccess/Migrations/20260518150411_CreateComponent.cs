using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HCM.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreateComponent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PayComponents",
                columns: table => new
                {
                    PayComponentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComponentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComponentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PayFormula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PayTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsStatutory = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    IsPayable = table.Column<bool>(type: "bit", nullable: true),
                    IsTaxable = table.Column<bool>(type: "bit", nullable: true),
                    hasPayComponentGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayMapTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MappingCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MappingName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MappingType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MappingTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayComponents", x => x.PayComponentId);
                });

            migrationBuilder.InsertData(
                table: "PayComponents",
                columns: new[] { "PayComponentId", "ComponentName", "ComponentType", "CreatedDate", "Description", "IsActive", "IsPayable", "IsStatutory", "IsTaxable", "MappingCode", "MappingName", "MappingType", "MappingTypeCode", "ModifiedDate", "PayFormula", "PayMapTo", "PayTypeCode", "hasPayComponentGroup" },
                values: new object[,]
                {
                    { 1, "Basic Salary", "Earning", new DateTime(2026, 5, 18, 20, 0, 0, 0, DateTimeKind.Local), null, null, null, null, null, null, null, null, null, null, "#Basic#", null, null, null },
                    { 2, "House Rent Allowance", "Earning", new DateTime(2026, 5, 18, 20, 0, 0, 0, DateTimeKind.Local), null, null, null, null, null, null, null, null, null, null, "##BASIC#*#50#%#", null, null, null },
                    { 3, "Medical Allowance", "Earning", new DateTime(2026, 5, 18, 20, 0, 0, 0, DateTimeKind.Local), null, null, null, null, null, null, null, null, null, null, "#", null, null, null },
                    { 4, "Provident Fund", "Deduction", new DateTime(2026, 5, 18, 20, 0, 0, 0, DateTimeKind.Local), null, null, null, null, null, null, null, null, null, null, "#", null, null, null },
                    { 5, "Professional Tax", "Deduction", new DateTime(2026, 5, 18, 20, 0, 0, 0, DateTimeKind.Local), null, null, null, null, null, null, null, null, null, null, "#", null, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PayComponents");
        }
    }
}
