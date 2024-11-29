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
    MongoDB,
    SQLite
}

public class RepositoryFactory
{
    private readonly IMongoDatabase _mongoDatabase;
    private readonly SqliteDbcontext _sqlContext;

    public RepositoryFactory(IMongoDatabase mongoDatabase, SqliteDbcontext sqlContext)
    {
        _mongoDatabase = mongoDatabase;
        _sqlContext = sqlContext;
    }

    public IRepository<Product> CreateRepository(DatabaseType databaseType)
    {
        return databaseType switch
        {
            DatabaseType.MongoDB => new MongoDbRepository(_mongoDatabase),
            DatabaseType.SQLite => new SqlRepository(_sqlContext),
            _ => throw new ArgumentException("Unsupported database type")
        };
    }
}


