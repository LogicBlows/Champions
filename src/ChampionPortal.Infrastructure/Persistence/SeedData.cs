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

    public static readonly Guid CompetencyDotNetId = new("33333333-cccc-cccc-cccc-000000000001");
    public static readonly Guid CompetencyJavaId = new("33333333-cccc-cccc-cccc-000000000002");
    public static readonly Guid CompetencyDataId = new("33333333-cccc-cccc-cccc-000000000003");
    public static readonly Guid CompetencyPhpId = new("33333333-cccc-cccc-cccc-000000000004");
    public static readonly Guid CompetencyDatabaseId = new("33333333-cccc-cccc-cccc-000000000005");

    public static void Apply(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Competency>().HasData(
    new Competency
    {
        ID = CompetencyDotNetId,
        Code = "dotnet",
        Name = "DotNet",
        Description = "Dotnet Developers",
        SortOrder = 1,
        isActive = true
    },
    new Competency
    {
        ID = CompetencyJavaId,
        Code = "java",
        Name = "Java",
        Description = "Java Developers",
        SortOrder = 2,
        isActive = true
    },
    new Competency
    {
        ID = CompetencyDataId,
        Code = "data",
        Name = "Data",
        Description = "Data Sceientist and Engineers",
        SortOrder = 3,
        isActive = true
    },
    new Competency
    {
        ID = CompetencyPhpId,
        Code = "php",
        Name = "PHP",
        Description = "PHP Developers",
        SortOrder = 4,
        isActive = true
    },
    new Competency
    {
        ID = CompetencyDatabaseId,
        Code = "database",
        Name = "Database",
        Description ="Database admins and developers",
        SortOrder = 5,
        isActive = true
    });

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
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000001"), EmployeeCode = "EMP001", Name = "Alice Admin", Email = "alice.reviewer@example.com", CompetencyId = CompetencyDotNetId, IsAdmin = true, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000002"), EmployeeCode = "EMP002", Name = "Bob Developer", Email = "bob.developer@example.com", CompetencyId = CompetencyDotNetId, IsAdmin = false, IsReviewer = true, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000003"), EmployeeCode = "EMP003", Name = "Priya Sharma", Email = "priya.sharma@example.com", CompetencyId = CompetencyDotNetId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000004"), EmployeeCode = "EMP004", Name = "Arjun Patel", Email = "arjun.patel@example.com", CompetencyId = CompetencyDotNetId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000005"), EmployeeCode = "EMP005", Name = "Ananya Reddy", Email = "ananya.reddy@example.com", CompetencyId = CompetencyDotNetId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000006"), EmployeeCode = "EMP006", Name = "Vikram Singh", Email = "vikram.singh@example.com", CompetencyId = CompetencyDotNetId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000007"), EmployeeCode = "EMP007", Name = "Emily Chen", Email = "emily.chen@example.com", CompetencyId = CompetencyDotNetId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000008"), EmployeeCode = "EMP008", Name = "Rohan Gupta", Email = "rohan.gupta@example.com", CompetencyId = CompetencyDotNetId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000009"), EmployeeCode = "EMP009", Name = "Kavya Iyer", Email = "kavya.iyer@example.com", CompetencyId = CompetencyJavaId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000010"), EmployeeCode = "EMP010", Name = "Aditya Joshi", Email = "aditya.joshi@example.com", CompetencyId = CompetencyJavaId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000011"), EmployeeCode = "EMP011", Name = "Meera Nair", Email = "meera.nair@example.com", CompetencyId = CompetencyJavaId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000012"), EmployeeCode = "EMP012", Name = "James Wilson", Email = "james.wilson@example.com", CompetencyId = CompetencyJavaId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000013"), EmployeeCode = "EMP013", Name = "Sanjay Krishnan", Email = "sanjay.krishnan@example.com", CompetencyId = CompetencyJavaId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000014"), EmployeeCode = "EMP014", Name = "Divya Menon", Email = "divya.menon@example.com", CompetencyId = CompetencyJavaId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000015"), EmployeeCode = "EMP015", Name = "Rahul Verma", Email = "rahul.verma@example.com", CompetencyId = CompetencyJavaId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000016"), EmployeeCode = "EMP016", Name = "Lisa Park", Email = "lisa.park@example.com", CompetencyId = CompetencyJavaId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000017"), EmployeeCode = "EMP017", Name = "Carol Analyst", Email = "carol.analyst@example.com", CompetencyId = CompetencyDataId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000018"), EmployeeCode = "EMP018", Name = "Isha Kapoor", Email = "isha.kapoor@example.com", CompetencyId = CompetencyDataId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000019"), EmployeeCode = "EMP019", Name = "Nikhil Desai", Email = "nikhil.desai@example.com", CompetencyId = CompetencyDataId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000020"), EmployeeCode = "EMP020", Name = "Pooja Bhat", Email = "pooja.bhat@example.com", CompetencyId = CompetencyDataId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000021"), EmployeeCode = "EMP021", Name = "Michael Torres", Email = "michael.torres@example.com", CompetencyId = CompetencyDataId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000022"), EmployeeCode = "EMP022", Name = "Shruti Pillai", Email = "shruti.pillai@example.com", CompetencyId = CompetencyDataId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000023"), EmployeeCode = "EMP023", Name = "Karthik Rao", Email = "karthik.rao@example.com", CompetencyId = CompetencyDataId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000024"), EmployeeCode = "EMP024", Name = "Neha Choudhary", Email = "neha.choudhary@example.com", CompetencyId = CompetencyDataId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000025"), EmployeeCode = "EMP025", Name = "Amit Shah", Email = "amit.shah@example.com", CompetencyId = CompetencyPhpId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000026"), EmployeeCode = "EMP026", Name = "Deepa Nambiar", Email = "deepa.nambiar@example.com", CompetencyId = CompetencyPhpId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000027"), EmployeeCode = "EMP027", Name = "Chris Evans", Email = "chris.evans@example.com", CompetencyId = CompetencyPhpId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000028"), EmployeeCode = "EMP028", Name = "Lakshmi Venkatesh", Email = "lakshmi.venkatesh@example.com", CompetencyId = CompetencyPhpId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000029"), EmployeeCode = "EMP029", Name = "Harish Malhotra", Email = "harish.malhotra@example.com", CompetencyId = CompetencyPhpId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000030"), EmployeeCode = "EMP030", Name = "Sneha Dutta", Email = "sneha.dutta@example.com", CompetencyId = CompetencyPhpId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000031"), EmployeeCode = "EMP031", Name = "Daniel Brooks", Email = "daniel.brooks@example.com", CompetencyId = CompetencyPhpId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000032"), EmployeeCode = "EMP032", Name = "Varun Agarwal", Email = "varun.agarwal@example.com", CompetencyId = CompetencyPhpId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000033"), EmployeeCode = "EMP033", Name = "Dave Champion", Email = "dave.champion@example.com", CompetencyId = CompetencyDatabaseId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000034"), EmployeeCode = "EMP034", Name = "Rajesh Iyer", Email = "rajesh.iyer@example.com", CompetencyId = CompetencyDatabaseId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000035"), EmployeeCode = "EMP035", Name = "Sunita Das", Email = "sunita.das@example.com", CompetencyId = CompetencyDatabaseId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000036"), EmployeeCode = "EMP036", Name = "Manish Trivedi", Email = "manish.trivedi@example.com", CompetencyId = CompetencyDatabaseId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000037"), EmployeeCode = "EMP037", Name = "Olivia Grant", Email = "olivia.grant@example.com", CompetencyId = CompetencyDatabaseId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000038"), EmployeeCode = "EMP038", Name = "Preeti Saxena", Email = "preeti.saxena@example.com", CompetencyId = CompetencyDatabaseId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000039"), EmployeeCode = "EMP039", Name = "Gaurav Mehta", Email = "gaurav.mehta@example.com", CompetencyId = CompetencyDatabaseId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate },
            new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-000000000040"), EmployeeCode = "EMP040", Name = "Aisha Khan", Email = "aisha.khan@example.com", CompetencyId = CompetencyDatabaseId, IsAdmin = false, IsReviewer = false, IsActive = true, CreatedOn = SeedDate });
    }
}
