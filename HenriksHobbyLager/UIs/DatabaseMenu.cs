using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace HenriksHobbyLager.UIs;

public class DatabaseMenu
{
    private readonly RepositoryFactory _repositoryFactory;
    
    public DatabaseMenu(RepositoryFactory repositoryFactory)
    {
        _repositoryFactory = repositoryFactory;
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
                    return DatabaseType.MongoDB;
                case "2":
                    return DatabaseType.SQLite;
                default:
                    Console.WriteLine("\nInvalid choice. Press any key to try again.");
                    Console.ReadKey();
                    continue;
            }
        }
    }
    public IRepository<Product> GetSelectedRepository()
    {
        var databaseType = ShowDatabaseMenu();
        return _repositoryFactory.CreateRepository(databaseType);
    }
}

