using HenriksHobbyLager.Models;
using Microsoft.EntityFrameworkCore;

namespace HenriksHobbyLager.Database
{
    public class SqliteDbContext : DbContext
    {
        public DbSet<ProductSQLite> Products { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\Users\\albin\\Code_Crasher_HenriksHobbyLager\\HenriksHobbyLager\\HenriksHobbyLager.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductSQLite>()
                .HasKey(p => p.Id); // Definiera Id som primärnyckel
        }
    }
}