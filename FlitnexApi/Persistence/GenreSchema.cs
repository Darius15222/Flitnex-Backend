using FlitnexApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlitnexApi.Persistence;

public class GenreSchema : IEntityTypeConfiguration<EGenre>
{
    public void Configure(EntityTypeBuilder<EGenre> builder)
    {
        builder.Property(g => g.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.HasMany(g => g.Movies)
            .WithMany(m => m.Genres);
            // .UsingEntity("MovieGenre",
            //     m => m.HasOne(typeof(EMovie)).WithMany()
            //         .HasForeignKey("MovieId"),
            //     
            //     m => m.HasOne(typeof(EGenre)).WithMany()
            //         .HasForeignKey("GenreId"),
            //     m =>
            //     {
            //         m.HasKey("GenreId", "MovieId");
            //         // m.ToTable("MovieGenre");
            //     });
    }
}