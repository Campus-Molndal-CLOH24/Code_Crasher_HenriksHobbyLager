using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HenriksHobbyLager.Database;
using Microsoft.EntityFrameworkCore;

namespace HenriksHobbyLager.Models
{  
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id); // Sätter Id som primärnyckel

            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired() // Gör Name obligatorisk
                .HasMaxLength(100); // Sätter max längd för Name

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18, 2)"); // Sätter datatyp för Price

            modelBuilder.Entity<Product>()
                .Property(p => p.Created)
                .HasDefaultValueSql("GETDATE()"); // Sätter standardvärde för Created till nuvarande datum

            modelBuilder.Entity<Product>()
                .Property(p => p.LastUpdated)
                .IsRequired(false); // LastUpdated kan vara null
        }
    }
}