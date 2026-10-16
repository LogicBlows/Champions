using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ChampionPortal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeCompetencyIdAndFortyEmployees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Competency",
                table: "Employees");

            migrationBuilder.AddColumn<Guid>(
                name: "CompetencyId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000001"),
                column: "CompetencyId",
                value: new Guid("33333333-cccc-cccc-cccc-000000000001"));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000002"),
                column: "CompetencyId",
                value: new Guid("33333333-cccc-cccc-cccc-000000000001"));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000003"),
                columns: new[] { "CompetencyId", "Email", "Name" },
                values: new object[] { new Guid("33333333-cccc-cccc-cccc-000000000001"), "priya.sharma@example.com", "Priya Sharma" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000004"),
                columns: new[] { "CompetencyId", "Email", "Name" },
                values: new object[] { new Guid("33333333-cccc-cccc-cccc-000000000001"), "arjun.patel@example.com", "Arjun Patel" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompetencyId", "CreatedOn", "Email", "EmployeeCode", "IsActive", "IsReviewer", "Name" },
                values: new object[,]
                {
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000005"), new Guid("33333333-cccc-cccc-cccc-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ananya.reddy@example.com", "EMP005", true, false, "Ananya Reddy" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000006"), new Guid("33333333-cccc-cccc-cccc-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "vikram.singh@example.com", "EMP006", true, false, "Vikram Singh" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000007"), new Guid("33333333-cccc-cccc-cccc-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "emily.chen@example.com", "EMP007", true, false, "Emily Chen" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000008"), new Guid("33333333-cccc-cccc-cccc-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "rohan.gupta@example.com", "EMP008", true, false, "Rohan Gupta" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000009"), new Guid("33333333-cccc-cccc-cccc-000000000002"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "kavya.iyer@example.com", "EMP009", true, false, "Kavya Iyer" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000010"), new Guid("33333333-cccc-cccc-cccc-000000000002"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "aditya.joshi@example.com", "EMP010", true, false, "Aditya Joshi" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000011"), new Guid("33333333-cccc-cccc-cccc-000000000002"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "meera.nair@example.com", "EMP011", true, false, "Meera Nair" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000012"), new Guid("33333333-cccc-cccc-cccc-000000000002"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "james.wilson@example.com", "EMP012", true, false, "James Wilson" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000013"), new Guid("33333333-cccc-cccc-cccc-000000000002"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "sanjay.krishnan@example.com", "EMP013", true, false, "Sanjay Krishnan" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000014"), new Guid("33333333-cccc-cccc-cccc-000000000002"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "divya.menon@example.com", "EMP014", true, false, "Divya Menon" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000015"), new Guid("33333333-cccc-cccc-cccc-000000000002"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "rahul.verma@example.com", "EMP015", true, false, "Rahul Verma" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000016"), new Guid("33333333-cccc-cccc-cccc-000000000002"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lisa.park@example.com", "EMP016", true, false, "Lisa Park" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000017"), new Guid("33333333-cccc-cccc-cccc-000000000003"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "carol.analyst@example.com", "EMP017", true, false, "Carol Analyst" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000018"), new Guid("33333333-cccc-cccc-cccc-000000000003"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "isha.kapoor@example.com", "EMP018", true, false, "Isha Kapoor" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000019"), new Guid("33333333-cccc-cccc-cccc-000000000003"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "nikhil.desai@example.com", "EMP019", true, false, "Nikhil Desai" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000020"), new Guid("33333333-cccc-cccc-cccc-000000000003"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "pooja.bhat@example.com", "EMP020", true, false, "Pooja Bhat" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000021"), new Guid("33333333-cccc-cccc-cccc-000000000003"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "michael.torres@example.com", "EMP021", true, false, "Michael Torres" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000022"), new Guid("33333333-cccc-cccc-cccc-000000000003"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "shruti.pillai@example.com", "EMP022", true, false, "Shruti Pillai" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000023"), new Guid("33333333-cccc-cccc-cccc-000000000003"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "karthik.rao@example.com", "EMP023", true, false, "Karthik Rao" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000024"), new Guid("33333333-cccc-cccc-cccc-000000000003"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "neha.choudhary@example.com", "EMP024", true, false, "Neha Choudhary" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000025"), new Guid("33333333-cccc-cccc-cccc-000000000004"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "amit.shah@example.com", "EMP025", true, false, "Amit Shah" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000026"), new Guid("33333333-cccc-cccc-cccc-000000000004"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "deepa.nambiar@example.com", "EMP026", true, false, "Deepa Nambiar" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000027"), new Guid("33333333-cccc-cccc-cccc-000000000004"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "chris.evans@example.com", "EMP027", true, false, "Chris Evans" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000028"), new Guid("33333333-cccc-cccc-cccc-000000000004"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lakshmi.venkatesh@example.com", "EMP028", true, false, "Lakshmi Venkatesh" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000029"), new Guid("33333333-cccc-cccc-cccc-000000000004"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "harish.malhotra@example.com", "EMP029", true, false, "Harish Malhotra" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000030"), new Guid("33333333-cccc-cccc-cccc-000000000004"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "sneha.dutta@example.com", "EMP030", true, false, "Sneha Dutta" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000031"), new Guid("33333333-cccc-cccc-cccc-000000000004"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "daniel.brooks@example.com", "EMP031", true, false, "Daniel Brooks" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000032"), new Guid("33333333-cccc-cccc-cccc-000000000004"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "varun.agarwal@example.com", "EMP032", true, false, "Varun Agarwal" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000033"), new Guid("33333333-cccc-cccc-cccc-000000000005"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "dave.champion@example.com", "EMP033", true, false, "Dave Champion" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000034"), new Guid("33333333-cccc-cccc-cccc-000000000005"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "rajesh.iyer@example.com", "EMP034", true, false, "Rajesh Iyer" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000035"), new Guid("33333333-cccc-cccc-cccc-000000000005"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "sunita.das@example.com", "EMP035", true, false, "Sunita Das" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000036"), new Guid("33333333-cccc-cccc-cccc-000000000005"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "manish.trivedi@example.com", "EMP036", true, false, "Manish Trivedi" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000037"), new Guid("33333333-cccc-cccc-cccc-000000000005"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "olivia.grant@example.com", "EMP037", true, false, "Olivia Grant" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000038"), new Guid("33333333-cccc-cccc-cccc-000000000005"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "preeti.saxena@example.com", "EMP038", true, false, "Preeti Saxena" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000039"), new Guid("33333333-cccc-cccc-cccc-000000000005"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "gaurav.mehta@example.com", "EMP039", true, false, "Gaurav Mehta" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000040"), new Guid("33333333-cccc-cccc-cccc-000000000005"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "aisha.khan@example.com", "EMP040", true, false, "Aisha Khan" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CompetencyId",
                table: "Employees",
                column: "CompetencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Competencies_CompetencyId",
                table: "Employees",
                column: "CompetencyId",
                principalTable: "Competencies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Competencies_CompetencyId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_CompetencyId",
                table: "Employees");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000005"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000006"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000007"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000008"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000009"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000010"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000011"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000012"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000013"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000014"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000015"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000016"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000017"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000018"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000019"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000020"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000021"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000022"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000023"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000024"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000025"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000026"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000027"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000028"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000029"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000030"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000031"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000032"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000033"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000034"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000035"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000036"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000037"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000038"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000039"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000040"));

            migrationBuilder.DropColumn(
                name: "CompetencyId",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "Competency",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000001"),
                column: "Competency",
                value: ".NET");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000002"),
                column: "Competency",
                value: ".NET");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000003"),
                columns: new[] { "Competency", "Email", "Name" },
                values: new object[] { "Data", "carol.analyst@example.com", "Carol Analyst" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-000000000004"),
                columns: new[] { "Competency", "Email", "Name" },
                values: new object[] { "Cloud", "dave.champion@example.com", "Dave Champion" });
        }
    }
}
