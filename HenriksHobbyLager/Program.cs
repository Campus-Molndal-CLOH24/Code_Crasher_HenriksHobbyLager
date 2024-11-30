
using HenriksHobbyLager.UIs;
using HenriksHobbyLager.Facade;
using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;
using HenriksHobbyLager.Database;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace HenriksHobbyLager
{
    static class Program
    {
        static void Main(string[] args)
        {
            // Create MongoDB context
            var mongoDbContext = new MongoDbContext();

            // Create SQL context
            var options = new DbContextOptionsBuilder<SqlDbcontext>()
                .UseSqlServer("Server=localhost\\SQLEXPRESS,1433;Database=ProductsHobbyLager;Integrated Security=True;TrustServerCertificate=True")
                .Options;
            var sqlContext = new SqlDbcontext(options);

            // Create SQLite context
            var sqliteOptions = new DbContextOptionsBuilder<SqliteDbcontext>()
                .UseSqlite("Data Source=ProductsHobbyLager.db")
                .Options;
            var sqliteContext = new SqliteDbcontext(sqliteOptions);

            // Create the DatabaseFactory with all required dependencies
            var databaseFactory = new DatabaseFactory(sqlContext, mongoDbContext, sqliteContext);

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




