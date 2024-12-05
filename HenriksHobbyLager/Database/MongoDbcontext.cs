using MongoDB.Driver;
using HenriksHobbyLager.Models;

namespace HenriksHobbyLager.Database
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext()
        {
            var client = new MongoClient("mongodb://localhost:27017/"); // Anpassa anslutningssträngen
            _database = client.GetDatabase("HenriksHobbyLager");
        }

        public IMongoCollection<ProductMongo> Products => _database.GetCollection<ProductMongo>("Products");
    }
}
