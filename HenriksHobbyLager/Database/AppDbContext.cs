using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenriksHobbyLager.Database
{
   
       // Den här klassen hanterar vår databasanslutning och tabellerna
        public class AppDbContext : DbContext
        {
            // En tabell för produkter
            public DbSet<Product> Products { get; set; }

            // Konfigurerar databasen och anger att vi använder SQLite
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite("Data Source=HobbyLager.db"); // Skapar filen HobbyLager.db
            }

            // Här kan vi anpassa våra tabeller, t.ex. primärnycklar och kolumnegenskaper
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Product>()
                    .HasKey(p => p.Id); // Sätter Id som primärnyckel

                modelBuilder.Entity<Product>()
                    .Property(p => p.Name)
                    .IsRequired() // Namn måste anges
                    .HasMaxLength(100); // Maxlängd för namn är 100 tecken

                modelBuilder.Entity<Product>()
                    .Property(p => p.Price)
                    .HasColumnType("decimal(18, 2)"); // Priset är ett decimaltal med 2 decimaler

                modelBuilder.Entity<Product>()
                    .Property(p => p.Created)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP"); // Skapas automatiskt med nuvarande datum
            }
        }
}
