using Microsoft.EntityFrameworkCore;
using HenriksHobbyLager.Models;


namespace HenriksHobbyLager.Database
{
    public class SqliteDbcontext : DbContext
    {
        public DbSet<Product> Product { get; set; } = null!;
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        
            => optionsBuilder.UseSqlite("Data Source=ProductsHobbyLager.db");
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure indexes for Product table
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name);  // Example: creating an index on the Name property
        }
    }
}