using HenriksHobbyLager.Models;
using Microsoft.EntityFrameworkCore;

public class SqliteDbContext : DbContext
{
    public DbSet<Product> Products { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=C:\\Users\\albin\\Code_Crasher_HenriksHobbyLager\\HenriksHobbyLager\\HenriksHobbyLager.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasKey(p => p.Id); // Definiera Id som primärnyckel
    }

    public SqliteDbContext()
    {
        // Utför migrering när en ny instans av kontexten skapas
        Database.Migrate();
    }
}
