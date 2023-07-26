using FlitnexApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlitnexApi.Persistence;

public class Context : DbContext
{
    public DbSet<EMovie> Movie { get; }
    public DbSet<EGenre> Genre { get; }
    public DbSet<ECast> Cast { get; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CastSchema());
        modelBuilder.ApplyConfiguration(new GenreSchema());
        modelBuilder.ApplyConfiguration(new MovieSchema());

        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        var entries = ChangeTracker.Entries().Where(e =>
            e.Entity is EBase && (e.State == EntityState.Added || e.State == EntityState.Modified));
        
        foreach (var entityEntry in entries)
        {
            ((EBase)entityEntry.Entity).UpdatedAt = DateTime.Now;

            if (entityEntry.State == EntityState.Added)
            {
                ((EBase)entityEntry.Entity).CreatedAt = DateTime.Now;
            }
        }
        return base.SaveChanges();
    }
}
