using Microsoft.EntityFrameworkCore;
using HenriksHobbyLager.Models;
using System.IO;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


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
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SqliteDbcontext>
    {
        public SqliteDbcontext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SqliteDbcontext>();

            // Use the same SQLite connection string as in your runtime code
            optionsBuilder.UseSqlite("Data Source=ProductsHobbyLager.db");

            return new SqliteDbcontext(optionsBuilder.Options);
        }
    }
}