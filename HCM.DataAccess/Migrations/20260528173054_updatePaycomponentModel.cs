using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCM.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updatePaycomponentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "hasPayComponentGroup",
                table: "PayComponents",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsTaxable",
                table: "PayComponents",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsStatutory",
                table: "PayComponents",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsPayable",
                table: "PayComponents",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "PayComponents",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "PayComponents",
                keyColumn: "PayComponentId",
                keyValue: 1,
                columns: new[] { "IsActive", "IsPayable", "IsStatutory", "IsTaxable", "hasPayComponentGroup" },
                values: new object[] { false, false, false, false, false });

            migrationBuilder.UpdateData(
                table: "PayComponents",
                keyColumn: "PayComponentId",
                keyValue: 2,
                columns: new[] { "IsActive", "IsPayable", "IsStatutory", "IsTaxable", "hasPayComponentGroup" },
                values: new object[] { false, false, false, false, false });

            migrationBuilder.UpdateData(
                table: "PayComponents",
                keyColumn: "PayComponentId",
                keyValue: 3,
                columns: new[] { "IsActive", "IsPayable", "IsStatutory", "IsTaxable", "hasPayComponentGroup" },
                values: new object[] { false, false, false, false, false });

            migrationBuilder.UpdateData(
                table: "PayComponents",
                keyColumn: "PayComponentId",
                keyValue: 4,
                columns: new[] { "IsActive", "IsPayable", "IsStatutory", "IsTaxable", "hasPayComponentGroup" },
                values: new object[] { false, false, false, false, false });

            migrationBuilder.UpdateData(
                table: "PayComponents",
                keyColumn: "PayComponentId",
                keyValue: 5,
                columns: new[] { "IsActive", "IsPayable", "IsStatutory", "IsTaxable", "hasPayComponentGroup" },
                values: new object[] { false, false, false, false, false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "hasPayComponentGroup",
                table: "PayComponents",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsTaxable",
                table: "PayComponents",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsStatutory",
                table: "PayComponents",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsPayable",
                table: "PayComponents",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "PayComponents",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                table: "PayComponents",
                keyColumn: "PayComponentId",
                keyValue: 1,
                columns: new[] { "IsActive", "IsPayable", "IsStatutory", "IsTaxable", "hasPayComponentGroup" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "PayComponents",
                keyColumn: "PayComponentId",
                keyValue: 2,
                columns: new[] { "IsActive", "IsPayable", "IsStatutory", "IsTaxable", "hasPayComponentGroup" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "PayComponents",
                keyColumn: "PayComponentId",
                keyValue: 3,
                columns: new[] { "IsActive", "IsPayable", "IsStatutory", "IsTaxable", "hasPayComponentGroup" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "PayComponents",
                keyColumn: "PayComponentId",
                keyValue: 4,
                columns: new[] { "IsActive", "IsPayable", "IsStatutory", "IsTaxable", "hasPayComponentGroup" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "PayComponents",
                keyColumn: "PayComponentId",
                keyValue: 5,
                columns: new[] { "IsActive", "IsPayable", "IsStatutory", "IsTaxable", "hasPayComponentGroup" },
                values: new object[] { null, null, null, null, null });
        }
    }
}
