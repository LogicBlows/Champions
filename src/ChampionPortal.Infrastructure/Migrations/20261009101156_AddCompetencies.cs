using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ChampionPortal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCompetencies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Competencies",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competencies", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "Competencies",
                columns: new[] { "ID", "Code", "Description", "Name", "SortOrder", "isActive" },
                values: new object[,]
                {
                    { new Guid("33333333-cccc-cccc-cccc-000000000001"), "dotnet", "Dotnet Developers", "DotNet", 1, true },
                    { new Guid("33333333-cccc-cccc-cccc-000000000002"), "java", "Java Developers", "Java", 2, true },
                    { new Guid("33333333-cccc-cccc-cccc-000000000003"), "data", "Data Sceientist and Engineers", "Data", 3, true },
                    { new Guid("33333333-cccc-cccc-cccc-000000000004"), "php", "PHP Developers", "PHP", 4, true },
                    { new Guid("33333333-cccc-cccc-cccc-000000000005"), "database", "Database admins and developers", "Database", 5, true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Competencies_Code",
                table: "Competencies",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Competencies");
        }
    }
}
