using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChampionPortal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCompetencyHeadEmployeeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "HeadEmployeeId",
                table: "Competencies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Competencies",
                keyColumn: "ID",
                keyValue: new Guid("33333333-cccc-cccc-cccc-000000000001"),
                column: "HeadEmployeeId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Competencies",
                keyColumn: "ID",
                keyValue: new Guid("33333333-cccc-cccc-cccc-000000000002"),
                column: "HeadEmployeeId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Competencies",
                keyColumn: "ID",
                keyValue: new Guid("33333333-cccc-cccc-cccc-000000000003"),
                column: "HeadEmployeeId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Competencies",
                keyColumn: "ID",
                keyValue: new Guid("33333333-cccc-cccc-cccc-000000000004"),
                column: "HeadEmployeeId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Competencies",
                keyColumn: "ID",
                keyValue: new Guid("33333333-cccc-cccc-cccc-000000000005"),
                column: "HeadEmployeeId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Competencies_HeadEmployeeId",
                table: "Competencies",
                column: "HeadEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Competencies_Employees_HeadEmployeeId",
                table: "Competencies",
                column: "HeadEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competencies_Employees_HeadEmployeeId",
                table: "Competencies");

            migrationBuilder.DropIndex(
                name: "IX_Competencies_HeadEmployeeId",
                table: "Competencies");

            migrationBuilder.DropColumn(
                name: "HeadEmployeeId",
                table: "Competencies");
        }
    }
}
