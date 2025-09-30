using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public class RegionConfiguration : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.ToTable("regions");
        builder.HasKey(r => r.Id);
        
        builder.Property(r => r.Name)
               .IsRequired()
               .HasColumnType("varchar(120)");

        builder.HasOne(r => r.Country)
                .WithMany(c => c.Regions)
                .HasForeignKey(r => r.CountryId)
                .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(ci => ci.Cities)
        .WithOne(r => r.Region)
        .HasForeignKey(ci => ci.RegionId)
        .OnDelete(DeleteBehavior.SetNull); 
    }
}