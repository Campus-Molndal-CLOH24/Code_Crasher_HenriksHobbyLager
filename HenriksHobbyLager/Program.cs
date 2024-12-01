
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.Sqlite;
using HenriksHobbyLager.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using HenriksHobbyLager.Repositories;
using RefactoringExercise.Repositories;

internal class Program
{
    private static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDbContext<AppDbContext>();

        var serviceProvider = serviceCollection.BuildServiceProvider();
    }
}

namespace HenriksHobbyLager
{
    public class Program
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Registrera ApplicationDbContext med SQLite
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
        }
    }


    // Definiera DbContext
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=HobbyLager.db"); // Konfigurera SQLite
        }

    }

    static void Main(string[] args)
    {
            // Ställer in tjänster och databaskonfiguration
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<ApplicationDbContext>();
            serviceCollection.AddScoped<Repository>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var repository = serviceProvider.GetService<Repository>();

            // Lägg till en produkt
            repository.Add(new Product { Name = "Pensel", Price = 20, Stock = 100, Category = "Målning" });

            // Visa alla produkter
            var products = repository.GetAll();
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Namn: {product.Name}, Pris: {product.Price:C}");
            }
    }

}




