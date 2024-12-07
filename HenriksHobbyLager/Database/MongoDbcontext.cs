using MongoDB.Bson;
using MongoDB.Driver;

using HenriksHobbyLager.Models;


namespace HenriksHobbyLager.Database
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        public MongoDbContext(IMongoDatabase database)
        {
            _database = database;
        }

        public IMongoCollection<Product> Products => _database.GetCollection<Product>("ProductsHobby");


        public async Task CreateIndexesAsync()
        {
            // Create a unique index on ProductName
        var indexKeysDefinition = Builders<Product>.IndexKeys.Ascending(p => p.Name);
        var indexOptions = new CreateIndexOptions { Unique = true, Background = true }; // Ensure uniqueness
        var createIndexModel = new CreateIndexModel<Product>(indexKeysDefinition, indexOptions);

        // Create the index for the Product collection
        await Products.Indexes.CreateOneAsync(createIndexModel); // Use the Products collection

        Console.WriteLine("Index on ProductName created successfully!");
        }

    }
}
//changed IConfiguration to IMongoDatabase, fot he database is passed to the context constructor. 