using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("companies");
        builder.HasKey(com => com.Id);

        builder.Property(com => com.Name)
               .IsRequired()
               .HasColumnType("varchar(120)");

              builder.Property(com => com.Nit)
               .IsRequired()
               .HasColumnType("varchar(120)");

        builder.Property(com => com.Address)
               .IsRequired()
               .HasColumnType("varchar(120)");
               
        builder.Property(com => com.Email)
               .IsRequired()
               .HasColumnType("varchar(150)");

        builder.HasOne(com => com.City)
            .WithMany(ci => ci.Companies)
            .HasForeignKey(com => com.CityId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(b => b.Branches)
        .WithOne(com => com.Company)
        .HasForeignKey(b => b.CompanyId)
        .OnDelete(DeleteBehavior.SetNull); 
        
        builder.HasIndex(c => c.Nit)
                   .IsUnique();
    }
}