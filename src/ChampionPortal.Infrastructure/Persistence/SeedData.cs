using ChampionPortal.Domain.Entities;
using ChampionPortal.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ChampionPortal.Infrastructure.Persistence;

public static class SeedData
{
    private static readonly DateTime SeedDate = new(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public static readonly Guid ActivityTypeWholeProjectId = new("11111111-aaaa-aaaa-aaaa-000000000001");
    public static readonly Guid ActivityTypeBugFixId = new("11111111-aaaa-aaaa-aaaa-000000000002");
    public static readonly Guid ActivityTypeAutomationScriptId = new("11111111-aaaa-aaaa-aaaa-000000000003");
    public static readonly Guid ActivityTypeExcelSpreadsheetId = new("11111111-aaaa-aaaa-aaaa-000000000004");
    public static readonly Guid ActivityTypeDocumentationId = new("11111111-aaaa-aaaa-aaaa-000000000005");
    public static readonly Guid ActivityTypeImageGenerationId = new("11111111-aaaa-aaaa-aaaa-000000000006");
    public static readonly Guid ActivityTypeOtherId = new("11111111-aaaa-aaaa-aaaa-000000000007");

    public static readonly Guid EmployeeAliceId = new("22222222-bbbb-bbbb-bbbb-000000000001");
    public static readonly Guid EmployeeBobId = new("22222222-bbbb-bbbb-bbbb-000000000002");
    public static readonly Guid EmployeeCarolId = new("22222222-bbbb-bbbb-bbbb-000000000003");
    public static readonly Guid EmployeeDaveId = new("22222222-bbbb-bbbb-bbbb-000000000004");

    public static void Apply(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityType>().HasData(
            new ActivityType
            {
                Id = ActivityTypeWholeProjectId,
                Name = "Whole Project",
                BasePoints = 100,
                Description = "End-to-end project delivery",
                Status = ActivityTypeStatus.Approved,
                ProposedByEmployeeId = null,
                IsActive = true,
                SortOrder = 1
            },
            new ActivityType
            {
                Id = ActivityTypeBugFixId,
                Name = "Bug Fix",
                BasePoints = 40,
                Description = "Defect resolution",
                Status = ActivityTypeStatus.Approved,
                ProposedByEmployeeId = null,
                IsActive = true,
                SortOrder = 2
            },
            new ActivityType
            {
                Id = ActivityTypeAutomationScriptId,
                Name = "Automation Script",
                BasePoints = 40,
                Description = "Script or automation tooling",
                Status = ActivityTypeStatus.Approved,
                ProposedByEmployeeId = null,
                IsActive = true,
                SortOrder = 3
            },
            new ActivityType
            {
                Id = ActivityTypeExcelSpreadsheetId,
                Name = "Excel / Spreadsheet",
                BasePoints = 25,
                Description = "Spreadsheet-based analysis or tooling",
                Status = ActivityTypeStatus.Approved,
                ProposedByEmployeeId = null,
                IsActive = true,
                SortOrder = 4
            },
            new ActivityType
            {
                Id = ActivityTypeDocumentationId,
                Name = "Documentation",
                BasePoints = 20,
                Description = "Technical or process documentation",
                Status = ActivityTypeStatus.Approved,
                ProposedByEmployeeId = null,
                IsActive = true,
                SortOrder = 5
            },
            new ActivityType
            {
                Id = ActivityTypeImageGenerationId,
                Name = "Image Generation",
                BasePoints = 15,
                Description = "AI-assisted image creation",
                Status = ActivityTypeStatus.Approved,
                ProposedByEmployeeId = null,
                IsActive = true,
                SortOrder = 6
            },
            new ActivityType
            {
                Id = ActivityTypeOtherId,
                Name = "Other",
                BasePoints = 10,
                Description = "Miscellaneous activity",
                Status = ActivityTypeStatus.Approved,
                ProposedByEmployeeId = null,
                IsActive = true,
                SortOrder = 7
            });

        modelBuilder.Entity<Employee>().HasData(
            new Employee
            {
                Id = EmployeeAliceId,
                EmployeeCode = "EMP001",
                Name = "Alice Reviewer",
                Email = "alice.reviewer@example.com",
                Competency = ".NET",
                IsReviewer = true,
                IsActive = true,
                CreatedOn = SeedDate
            },
            new Employee
            {
                Id = EmployeeBobId,
                EmployeeCode = "EMP002",
                Name = "Bob Developer",
                Email = "bob.developer@example.com",
                Competency = ".NET",
                IsReviewer = false,
                IsActive = true,
                CreatedOn = SeedDate
            },
            new Employee
            {
                Id = EmployeeCarolId,
                EmployeeCode = "EMP003",
                Name = "Carol Analyst",
                Email = "carol.analyst@example.com",
                Competency = "Data",
                IsReviewer = false,
                IsActive = true,
                CreatedOn = SeedDate
            },
            new Employee
            {
                Id = EmployeeDaveId,
                EmployeeCode = "EMP004",
                Name = "Dave Champion",
                Email = "dave.champion@example.com",
                Competency = "Cloud",
                IsReviewer = false,
                IsActive = true,
                CreatedOn = SeedDate
            });
    }
}
