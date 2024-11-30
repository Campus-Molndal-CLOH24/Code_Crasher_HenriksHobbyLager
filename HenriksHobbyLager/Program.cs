
using HenriksHobbyLager.UIs;
using HenriksHobbyLager.Facade;
using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;
using HenriksHobbyLager.Database;

namespace HenriksHobbyLager
{
    static class Program
    {
        static void Main(string[] args)
        {
            // Create MongoDB context
            var mongoDbContext = new MongoDbContext();

            // Create SQLite context
            var options = new DbContextOptionsBuilder<SqlDbcontext>()
                .UseSqlite("Data Source=ProductsHobbyLager.db")
                .Options;
            var sqlContext = new SqlDbcontext(options);

            // Create the DatabaseFactory with all required dependencies
            var databaseFactory = new DatabaseFactory(sqlContext, mongoDbContext);

            // Get repository through DatabaseMenu
            var databaseMenu = new DatabaseMenu(databaseFactory);
            var repository = databaseMenu.GetSelectedRepository();

            // Create facade with repository
            var productFacade = new ProductFacade(repository);

            // Instantiate ConsoleMenuHandler with dependencies
            var menuHandler = new ConsoleMenuHandler(productFacade, databaseFactory);

            // Show the main menu
            menuHandler.ShowMainMenu();
        }
    }
}




