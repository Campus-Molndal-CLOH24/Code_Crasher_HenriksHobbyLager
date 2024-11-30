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
    private readonly MongoDbContext _mongoDbContext;
    
    private readonly SqliteDbcontext _sqliteDbcontext;

    public DatabaseFactory(SqliteDbcontext sqliteDbcontext, MongoDbContext mongoDbContext)
    {
        _mongoDbContext = mongoDbContext;
        
        _sqliteDbcontext = sqliteDbcontext;
    }

    public IRepository<Product> CreateRepository(DatabaseType databaseType)
    {
        if (databaseType == DatabaseType.MongoDB)
        {
        
            return new MongoDbRepository(_mongoDbContext);
        }
        if (databaseType == DatabaseType.SQLite)
        {
            return new SqlRepository(_sqliteDbcontext); // Using the injected SqliteDbcontext
        }
        throw new InvalidOperationException("Unsupported database type");
    }
}
