using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Repository;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace HenriksHobbyLager.UIs;

public class DatabaseMenu
{
    private readonly DatabaseFactory _databaseFactory;

    public DatabaseMenu(DatabaseFactory databaseFactory)
    {
        _databaseFactory = databaseFactory;
    }
    public static DatabaseType ShowDatabaseMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Select Database Type: ");
            Console.WriteLine("1. SQL");
            Console.WriteLine("2. MongoDB");
            Console.Write("Enter your choice (1 or 2): ");
            string choice = Console.ReadLine() ?? string.Empty;

            switch (choice)
            {
                case "1":
                    return DatabaseType.SQLite;
                case "2":
                    return DatabaseType.MongoDB;
                default:
                    Console.WriteLine("\nInvalid choice. Press any key to try again.");
                    Console.ReadKey();
                    continue;
            }
        }
    }
    // Get the appropriate repository based on the database choice
    public IRepository<Product> GetSelectedRepository()
    {
        var databaseType = ShowDatabaseMenu();
        return _databaseFactory.CreateRepository(databaseType);
    }
}

