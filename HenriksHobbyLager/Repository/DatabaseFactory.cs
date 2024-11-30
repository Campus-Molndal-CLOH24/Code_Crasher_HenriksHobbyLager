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
    SQL = 2
}

public class DatabaseFactory
{
    private readonly MongoDbContext _mongoDbContext;
    
    private readonly SqlDbcontext _sqlDbcontext;

    public DatabaseFactory(SqlDbcontext sqlDbcontext, MongoDbContext mongoDbContext)
    {
        _mongoDbContext = mongoDbContext;
        
        _sqlDbcontext = sqlDbcontext;
    }

    public IRepository<Product> CreateRepository(DatabaseType databaseType)
    {
        if (databaseType == DatabaseType.MongoDB)
        {
        
            return new MongoDbRepository(_mongoDbContext);
        }
        if (databaseType == DatabaseType.SQL)
        {
            return new SqlRepository(_sqlDbcontext); // Using the injected SqliteDbcontext
        }
        throw new InvalidOperationException("Unsupported database type");
    }
}
