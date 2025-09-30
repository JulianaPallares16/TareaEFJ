using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("countries");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name)
               .IsRequired()
               .HasColumnType("varchar(120)");

        builder.HasMany(r => r.Regions)
        .WithOne(c => c.Country)
        .HasForeignKey(r => r.CountryId)
        .OnDelete(DeleteBehavior.SetNull); 
    }
}
