using ChampionPortal.Domain.Entities;
using ChampionPortal.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChampionPortal.Infrastructure.Persistence.Configurations;

public class ActivityLogConfiguration : IEntityTypeConfiguration<ActivityLog>
{
    public void Configure(EntityTypeBuilder<ActivityLog> builder)
    {
        builder.Property(a => a.Status)
            .HasConversion<string>();

        builder.Property(a => a.Source)
            .HasConversion<string>();

        builder.HasOne(a => a.Employee)
            .WithMany(e => e.ActivityLogs)
            .HasForeignKey(a => a.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasOne(a => a.ActivityType)
            .WithMany()
            .HasForeignKey(a => a.ActivityTypeId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}
