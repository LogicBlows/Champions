using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ChampionPortal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasePoints = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProposedByEmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SuggestedPoints = table.Column<int>(type: "int", nullable: true),
                    ReviewedByEmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReviewedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Competency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsReviewer = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivityTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PointsAwarded = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmittedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityLogs_ActivityTypes_ActivityTypeId",
                        column: x => x.ActivityTypeId,
                        principalTable: "ActivityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityLogs_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Endorsements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivityLogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndorsedByEmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndorsedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endorsements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Endorsements_ActivityLogs_ActivityLogId",
                        column: x => x.ActivityLogId,
                        principalTable: "ActivityLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ActivityTypes",
                columns: new[] { "Id", "BasePoints", "Description", "IsActive", "Name", "ProposedByEmployeeId", "ReviewedByEmployeeId", "ReviewedOn", "SortOrder", "Status", "SuggestedPoints" },
                values: new object[,]
                {
                    { new Guid("11111111-aaaa-aaaa-aaaa-000000000001"), 100, "End-to-end project delivery", true, "Whole Project", null, null, null, 1, "Approved", null },
                    { new Guid("11111111-aaaa-aaaa-aaaa-000000000002"), 40, "Defect resolution", true, "Bug Fix", null, null, null, 2, "Approved", null },
                    { new Guid("11111111-aaaa-aaaa-aaaa-000000000003"), 40, "Script or automation tooling", true, "Automation Script", null, null, null, 3, "Approved", null },
                    { new Guid("11111111-aaaa-aaaa-aaaa-000000000004"), 25, "Spreadsheet-based analysis or tooling", true, "Excel / Spreadsheet", null, null, null, 4, "Approved", null },
                    { new Guid("11111111-aaaa-aaaa-aaaa-000000000005"), 20, "Technical or process documentation", true, "Documentation", null, null, null, 5, "Approved", null },
                    { new Guid("11111111-aaaa-aaaa-aaaa-000000000006"), 15, "AI-assisted image creation", true, "Image Generation", null, null, null, 6, "Approved", null },
                    { new Guid("11111111-aaaa-aaaa-aaaa-000000000007"), 10, "Miscellaneous activity", true, "Other", null, null, null, 7, "Approved", null }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Competency", "CreatedOn", "Email", "EmployeeCode", "IsActive", "IsReviewer", "Name" },
                values: new object[,]
                {
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000001"), ".NET", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "alice.reviewer@example.com", "EMP001", true, true, "Alice Reviewer" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000002"), ".NET", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "bob.developer@example.com", "EMP002", true, false, "Bob Developer" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000003"), "Data", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "carol.analyst@example.com", "EMP003", true, false, "Carol Analyst" },
                    { new Guid("22222222-bbbb-bbbb-bbbb-000000000004"), "Cloud", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "dave.champion@example.com", "EMP004", true, false, "Dave Champion" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_ActivityTypeId",
                table: "ActivityLogs",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_EmployeeId",
                table: "ActivityLogs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeCode",
                table: "Employees",
                column: "EmployeeCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Endorsements_ActivityLogId",
                table: "Endorsements",
                column: "ActivityLogId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Endorsements");

            migrationBuilder.DropTable(
                name: "ActivityLogs");

            migrationBuilder.DropTable(
                name: "ActivityTypes");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
