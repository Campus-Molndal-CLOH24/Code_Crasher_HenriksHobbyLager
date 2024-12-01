using MongoDB.Bson;
using MongoDB.Driver;
using System;
using HenriksHobbyLager.Models;
using Microsoft.Extensions.Configuration;

namespace HenriksHobbyLager.Database
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        public MongoDbContext(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MongoDb"));
            _database = client.GetDatabase("HenriksHobbyLager");
        }

        public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");

    }
}