using HenriksHobbyLager.Models;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace HenriksHobbyLager.Database;

public class SqliteDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=HenriksHobbyLager.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasKey(p => p.Id); // Definiera Id som primärnyckel
    }
}
