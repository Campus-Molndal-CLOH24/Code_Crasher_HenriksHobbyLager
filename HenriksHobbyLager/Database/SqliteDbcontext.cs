using Microsoft.EntityFrameworkCore;


namespace HenriksHobbyLager.Database
{
    public class SqliteDbcontext : DbContext
    {
        public DbSet<Product> Product { get; set; } = null!;
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=ProductsHobbyLager.db");
        }
    }
}