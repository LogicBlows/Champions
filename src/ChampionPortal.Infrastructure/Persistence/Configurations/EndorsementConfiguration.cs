using ChampionPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChampionPortal.Infrastructure.Persistence.Configurations;

public class EndorsementConfiguration : IEntityTypeConfiguration<Endorsement>
{
    public void Configure(EntityTypeBuilder<Endorsement> builder)
    {
        builder.HasOne(e => e.ActivityLog)
            .WithMany(a => a.Endorsements)
            .HasForeignKey(e => e.ActivityLogId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
