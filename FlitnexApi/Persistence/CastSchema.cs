using FlitnexApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlitnexApi.Persistence;

public class CastSchema : IEntityTypeConfiguration<ECast>
{
    public void Configure(EntityTypeBuilder<ECast> builder)
    {
        builder.Property(p => p.Name).HasMaxLength(255).IsRequired();
        builder.Property(p => p.Role).HasMaxLength(150).IsRequired();
    }
}