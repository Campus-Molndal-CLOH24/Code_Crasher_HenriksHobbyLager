using HenriksHobbyLager.UIs;
using HenriksHobbyLager.Facade;
using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;
using HenriksHobbyLager.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HenriksHobbyLager.Repository;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using MongoDB.Bson;
using DotNetEnv;

namespace HenriksHobbyLager
{
    static class Program
    {
        static async Task Main(string[] args)
        {

            Env.Load();
            // Connection strings for SQLite and MongoDB (you can replace them with your actual strings)
            string sqliteConnectionString = "Data Source=mydatabase.db"; // SQLite connection string
            string mongoDbConnectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING") ?? string.Empty;
            

            if (string.IsNullOrEmpty(mongoDbConnectionString))
            {
                Console.WriteLine("MongoDB connection string is not set. Exiting.");
                return;
            }

            Console.WriteLine("Which database would you like to use? (1 for MongoDB, 2 for SQLite)");
            string choice = Console.ReadLine() ?? string.Empty;

            IRepository<Product> repository;

            if (choice == "1")
            {
                // Create MongoDB client and database
                var client = new MongoClient(mongoDbConnectionString);
                var database = client.GetDatabase("productdb");
                var context = new MongoDbContext(database); // Create the context
                repository = new MongoDbRepository(context); // Directly instantiate the repository
            }
            else if (choice == "2")
            {
                var optionsBuilder = new DbContextOptionsBuilder<SqliteDbcontext>();
                optionsBuilder.UseSqlite(sqliteConnectionString); // Set the SQLite connection string

                var sqliteContext = new SqliteDbcontext(optionsBuilder.Options); // Pass the options
                repository = new SqliteRepository(sqliteContext);
            }
            else
            {
                Console.WriteLine("Invalid choice");
                return;
            }

            var facade = new ProductFacade(repository); // Pass the repository to the facade
            var menu = new ConsoleMenuHandler(facade);
            await menu.ShowMainMenu(); // Assuming ShowMainMenu is async
        }


    }
}




