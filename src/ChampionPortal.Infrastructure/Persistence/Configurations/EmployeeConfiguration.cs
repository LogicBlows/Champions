using ChampionPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChampionPortal.Infrastructure.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasIndex(e => e.EmployeeCode)
            .IsUnique();


        builder.HasOne(e => e.Competency)
            .WithMany()
            .HasForeignKey(e => e.CompetencyId)
            .HasPrincipalKey(c => c.ID)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
