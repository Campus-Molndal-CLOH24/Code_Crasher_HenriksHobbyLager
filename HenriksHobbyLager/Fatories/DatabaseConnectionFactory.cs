using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using HenriksHobbyLager.Database;



namespace HenriksHobbyLager.Repository;



public class DatabaseConnectionFactory
{
    public enum DatabaseType
    {
        Sqlite,
        MongoDb
    }
    private static readonly IConfiguration _config = LoadConfiguration();
    //get adviced this instance is better and safer


    public static object GetDbContext(string databaseType)
    {
        return databaseType.ToLower() switch
        {
            "mongodb" => new MongoDbContext(_config),  // Return MongoDbContext for MongoDB
            "sqlite" => CreateSqliteDbContext(),       // Return SqliteDbContext for SQLite
            _ => throw new ArgumentException($"Unknown database type: {databaseType}")
        };
    }
    private static SqliteDbcontext CreateSqliteDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<SqliteDbcontext>();
        optionsBuilder.UseSqlite(_config.GetConnectionString("SQLite"));
        return new SqliteDbcontext(optionsBuilder.Options);
    }

    private static IConfiguration LoadConfiguration()
    {
        try
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Unable to load database configuration", ex);
        }
    }

}
