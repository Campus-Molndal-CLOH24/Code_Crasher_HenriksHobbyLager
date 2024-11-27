using Microsoft.EntityFrameworkCore;
using HenriksHobbyLager.Models;

namespace HenriksHobbyLager.Database
{
    public class ApplicationDbcontext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=HobbyLager.db");
        }
    }
}