using MongoDB.Driver;
using HenriksHobbyLager.Models;

namespace HenriksHobbyLager.Database
{
    public class MongoDbContext
    {
        
        private readonly IMongoCollection<Product> _products;

        public MongoDbContext()
        {
            // Connect to MongoDB (running locally)
            var connectionString = "mongodb+srv://yotaka:Johansson99*@cluster0.mongodb.net/HenriksHobbyLager";
            var client = new MongoClient(connectionString);

            
            // Get or create database
            var database = client.GetDatabase("HenriksHobbyLager");
            
            // Get or create collection (similar to table in SQL)
            _products = database.GetCollection<Product>("Products");
        }

        // Property to access Products collection
        public IMongoCollection<Product> Products => _products;
    }
}

