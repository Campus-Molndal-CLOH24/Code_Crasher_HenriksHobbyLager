using HenriksHobbyLager.Database;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Repository;
using HenriksHobbyLager.Service;// För att använda MenuService
using HenriksHobbyLager.UIs;
using MongoDB.Driver;

namespace HenriksHobbyLager
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Välj databas: 1 för SQLite, 2 för MongoDB");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                // SQLite-integration
                var sqliteContext = new SqliteDbContext(); // Din SQLite DbContext
                var sqliteRepository = new SQLiteRepository(sqliteContext);
                var productRepository = new ProductRepository<ProductSQLite>(sqliteRepository);
                var productService = new ProductService<ProductSQLite>(productRepository);
                var menuService = new MenuService<ProductSQLite>(productService);

                // Kör menyn
                menuService.DisplayMenu();
            }
            else if (choice == "2")
            {
                // MongoDB-integration
                var client = new MongoClient("mongodb://localhost:27017");
                var database = client.GetDatabase("HobbyLager");
                var mongoRepository = new MongoDBRepository(database);
                var productRepository = new ProductRepository<ProductMongo>(mongoRepository);
                var productService = new ProductService<ProductMongo>(productRepository);
                var menuService = new MenuService<ProductMongo>(productService);

                // Kör menyn
                menuService.DisplayMenu();
            }
            else
            {
                Console.WriteLine("Ogiltigt val! Avslutar...");
            }
        }
    }
}
