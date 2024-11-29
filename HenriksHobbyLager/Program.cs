
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.Sqlite;
using HenriksHobbyLager.Database;
using Microsoft.EntityFrameworkCore;

namespace HenriksHobbyLager
{
    // Definiera DbContext
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=HobbyLager.db"); // Konfigurera SQLite
        }

    }



}


        

        