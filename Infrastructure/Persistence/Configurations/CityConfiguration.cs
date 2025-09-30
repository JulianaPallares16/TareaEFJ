using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("cities");
        builder.HasKey(ci => ci.Id);
        
        builder.Property(ci => ci.Name)
               .IsRequired()
               .HasColumnType("varchar(120)");

        builder.HasOne(ci => ci.Region)
                .WithMany(r => r.Cities)
                .HasForeignKey(ci => ci.RegionId)
                .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(com => com.Companies)
        .WithOne(ci => ci.City)
        .HasForeignKey(com => com.CityId)
        .OnDelete(DeleteBehavior.SetNull); 

        builder.HasMany(b => b.Branches)
        .WithOne(ci => ci.City)
        .HasForeignKey(b => b.CityId)
        .OnDelete(DeleteBehavior.SetNull); 
    }
}