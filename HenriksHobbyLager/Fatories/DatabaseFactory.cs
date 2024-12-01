using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using HenriksHobbyLager.Database;
using Microsoft.EntityFrameworkCore;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Repository;
using HenriksHobbyLager.Interfaces;

namespace HenriksHobbyLager.Repository;

public enum DatabaseType
{
    MongoDB = 1,
    SQL = 2,
    SQLite = 3
}

public class DatabaseFactory
{
    
    private readonly SqlDbcontext _sqlContext;
    private readonly MongoDbContext _mongoContext;
    private readonly SqliteDbcontext _sqliteContext;
   

    public DatabaseFactory( SqlDbcontext sqlContext, MongoDbContext mongoContext, SqliteDbcontext sqliteContext)
    {
        
        _sqlContext = sqlContext;
        _mongoContext = mongoContext;
        _sqliteContext = sqliteContext;
        
    }

    public IRepository<Product> CreateRepository(DatabaseType databaseType)
    {
        switch (databaseType)
        {
            case DatabaseType.MongoDB:
                return new MongoDbRepository(_mongoContext);
            case DatabaseType.SQLite:
                return new SqliteRepository(_sqliteContext);
            case DatabaseType.SQL:
                return new SqlRepository(_sqlContext);
            default:
                throw new ArgumentException("Unsupported database type");
        }
    }
}
