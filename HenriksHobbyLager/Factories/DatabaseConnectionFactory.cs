using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;
using HenriksHobbyLager.Repository;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Database;

namespace HenriksHobbyLager.Repository
{
    public class DatabaseConnectionFactory
    {
        private readonly string _sqliteConnectionString = "Data Source=ProductsHobbyLager.db"; 
        private readonly string _mongoDbConnectionString = "mongodb+srv://yotaka:Johansson99@cluster0.mongodb.net/HenriksHobbyLager?retryWrites=true&w=majority"; // Ensure correct URL
        public DatabaseConnectionFactory(string sqliteConnectionString, string mongoDbConnectionString)
        {
            _sqliteConnectionString = sqliteConnectionString ?? throw new ArgumentNullException(nameof(sqliteConnectionString));
            _mongoDbConnectionString = mongoDbConnectionString ?? throw new ArgumentNullException(nameof(mongoDbConnectionString));
        }
        // Get the appropriate repository based on the configuration
        public IRepository<Product> GetRepository(string dbType)
        {
            if (string.IsNullOrEmpty(dbType))
            {
                throw new ArgumentException("Database type is required.");
            }

            return dbType.ToLowerInvariant() switch
            {
                "sqlite" => new SqliteRepository(CreateSqliteDbContext()),
                "mongodb" => new MongoDbRepository(CreateMongoDbContext()),
                _ => throw new ArgumentException($"Unknown database type: {dbType}")
            };
        }

        // Create SqliteDbContext
        private SqliteDbcontext CreateSqliteDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SqliteDbcontext>();
            optionsBuilder.UseSqlite(_sqliteConnectionString);
            return new SqliteDbcontext(optionsBuilder.Options);
        }

        // Create MongoDbContext 
        private MongoDbContext CreateMongoDbContext()
        {
            try
            {
                var client = new MongoClient(_mongoDbConnectionString);
                var database = client.GetDatabase("HenriksHobbyLager");
                return new MongoDbContext(database);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to connect to MongoDB", ex);
            }
        }
    }
}
