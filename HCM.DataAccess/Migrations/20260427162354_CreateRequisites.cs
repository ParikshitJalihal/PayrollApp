using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HCM.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreateRequisites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Requesites",
                columns: table => new
                {
                    ReqId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReqName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReqDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requesites", x => x.ReqId);
                });

            migrationBuilder.CreateTable(
                name: "RequisiteDetails",
                columns: table => new
                {
                    RequisiteDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReqId = table.Column<int>(type: "int", nullable: true),
                    RequisiteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequisiteValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequisiteDetails", x => x.RequisiteDetailsId);
                    table.ForeignKey(
                        name: "FK_RequisiteDetails_Requesites_ReqId",
                        column: x => x.ReqId,
                        principalTable: "Requesites",
                        principalColumn: "ReqId");
                });

            migrationBuilder.InsertData(
                table: "Requesites",
                columns: new[] { "ReqId", "CreatedDate", "ReqDescription", "ReqName" },
                values: new object[,]
                {
                    { 1, null, "", "Assets" },
                    { 2, null, "", "Department" },
                    { 3, null, "", "Designation" },
                    { 4, null, "", "Gender" }
                });

            migrationBuilder.InsertData(
                table: "RequisiteDetails",
                columns: new[] { "RequisiteDetailsId", "CreatedDate", "ReqId", "RequisiteName", "RequisiteValue" },
                values: new object[,]
                {
                    { 1, null, 1, null, "Laptop" },
                    { 2, null, 1, null, "Mouse" },
                    { 3, null, 2, null, "HR" },
                    { 4, null, 2, null, "Finance" },
                    { 5, null, 2, null, "Operations" },
                    { 6, null, 2, null, "Technical" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequisiteDetails_ReqId",
                table: "RequisiteDetails",
                column: "ReqId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequisiteDetails");

            migrationBuilder.DropTable(
                name: "Requesites");
        }
    }
}
