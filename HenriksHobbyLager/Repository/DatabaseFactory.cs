using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using HenriksHobbyLager.Database;
using Microsoft.EntityFrameworkCore;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Repository;
using HenriksHobbyLager.Interfaces;

namespace HenriksHobbyLager;

public enum DatabaseType
{
    MongoDB = 1,
    SQLite = 2
}

public class DatabaseFactory
{
    private readonly IConfiguration _configuration;

    public DatabaseFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IRepository<Product> CreateRepository(DatabaseType databaseType)
    {
        if (databaseType == DatabaseType.MongoDB)
        {
            var mongoClient = new MongoClient(_configuration.GetConnectionString("MongoConnection"));
            var mongoDatabase = mongoClient.GetDatabase(_configuration.GetConnectionString("MongoDatabase"));
            return new MongoDbRepository(mongoDatabase);
        }
        if (databaseType == DatabaseType.SQLite)
        {
            var sqlContext = new SqliteDbcontext(
                new DbContextOptionsBuilder<SqliteDbcontext>()
                    .UseSqlite(_configuration.GetConnectionString("SqliteConnection"))
                    .Options);
            return new SqlRepository(sqlContext);
        }
        throw new ArgumentException("Unsupported database type");
    }
}


