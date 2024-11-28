using Microsoft.EntityFrameworkCore;
using HenriksHobbyLager.Models;


namespace HenriksHobbyLager.Database
{
    public class SqliteDbcontext : DbContext
    {
        public DbSet<Product> Product { get; set; } = null!;
       

        // Constructor that accepts DbContextOptions
        public SqliteDbcontext(DbContextOptions<SqliteDbcontext> options) 
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

            => optionsBuilder.UseSqlite("Data Source=ProductsHobbyLager.db");
        // Configure indexes for Product table (task 2.3.2)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure indexes for Product table
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name);  // creating an index on the Name property
        }
    }
}