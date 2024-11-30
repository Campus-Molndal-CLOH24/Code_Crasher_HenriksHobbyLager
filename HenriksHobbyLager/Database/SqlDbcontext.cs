using Microsoft.EntityFrameworkCore;
using HenriksHobbyLager.Models;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.SqlServer;


namespace HenriksHobbyLager.Database
{
    public class SqlDbcontext : DbContext
    {
        //help prevent null reference exceptions by making nullability explicit in my code.
        // i get waring when i ran dotnet build and it showed that i need to set null! to Product 
        // becuase it is a nullable reference type. 
        public DbSet<Product> Product { get; set; } = null!;


        // Constructor that accepts DbContextOptions
        // need it becuase need to create instance of SqliteDbcontext and makes it possible to configure the DbContext
        // and pass the configuration to the base class DbContext. EF won't know how to create the instance.
        //if we delect it we get error on the console menu handler.
        public SqlDbcontext(DbContextOptions<SqlDbcontext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

            => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS,1433;Database=ProductsHobbyLager;Integrated Security=True;TrustServerCertificate=True");
        // Configure indexes for Product table (task 2.3.2)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure indexes for Product table
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name)
                .IsUnique();  // creating an index on the Name property
        }
    }
    // DesignTimeDbContextFactory for migrations , it shows on error when i ran ef migrations add that i need to add thses class for 
    // run migrations it about the datetime too when we creted entity get rekommenn from error and checked on microsoft docu
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SqlDbcontext>
    {
        public SqlDbcontext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SqlDbcontext>();

            // Use the same SQLite connection string as in your runtime code
            optionsBuilder.UseSqlite("Server=localhost\\SQLEXPRESS,1433;Database=ProductsHobbyLager;Integrated Security=True;TrustServerCertificate=True");

            return new SqlDbcontext(optionsBuilder.Options);
        }
    }
}