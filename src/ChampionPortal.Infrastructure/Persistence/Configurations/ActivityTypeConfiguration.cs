using ChampionPortal.Domain.Entities;
using ChampionPortal.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChampionPortal.Infrastructure.Persistence.Configurations;

public class ActivityTypeConfiguration : IEntityTypeConfiguration<ActivityType>
{
    public void Configure(EntityTypeBuilder<ActivityType> builder)
    {
        builder.Property(a => a.Status)
            .HasConversion<string>();
    }
}
