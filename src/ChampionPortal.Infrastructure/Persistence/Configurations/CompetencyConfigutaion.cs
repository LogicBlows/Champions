using ChampionPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionPortal.Infrastructure.Persistence.Configurations
{
    public class CompetencyConfigutaion : IEntityTypeConfiguration<Competency>
    {
        public void Configure(EntityTypeBuilder<Competency> builder)
        {

            builder.Property(c =>c.Code)
                .HasMaxLength(32).IsRequired();

            builder.Property(c => c.Name)
           .HasMaxLength(100)
           .IsRequired();


            builder.HasIndex(c => c.Code)
                .IsUnique();

            builder.Property(c => c.Description)
            .HasMaxLength(500)
            .IsRequired();

            builder.HasOne<Employee>()
                .WithMany()
                .HasForeignKey(c => c.HeadEmployeeId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
