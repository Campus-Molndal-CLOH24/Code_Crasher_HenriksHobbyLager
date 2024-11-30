using MongoDB.Bson;
using MongoDB.Driver;
using System;
using HenriksHobbyLager.Models;

namespace HenriksHobbyLager.Database
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;


        // Constructor to initialize MongoDB client
        public MongoDbContext(string connectionString, string databaseName)
        {
            try
            {
                var client = new MongoClient(connectionString);
                _database = client.GetDatabase(databaseName);
                Console.WriteLine("MongoDB connection successful!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MongoDB connection failed: {ex.Message}");
                throw;
            }
        }

        // Method to insert a document into the MongoDB database
        public void InsertDocument(BsonDocument document)
        {
            try
            {
                // Access the collection (create if not exists)
                var collection = _database.GetCollection<BsonDocument>("HobbyLager");

                // Insert the document into the collection
                collection.InsertOne(document);
                Console.WriteLine("Document inserted successfully!");

                // Verify by reading all documents
                var documents = collection.Find(new BsonDocument()).ToList();
                Console.WriteLine("All documents in MongoDB:");
                foreach (var doc in documents)
                {
                    Console.WriteLine(doc.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting document: {ex.Message}");
            }
        }
        public IMongoCollection<Product> Products
        {
            get
            {
                return _database.GetCollection<Product>("HobbyLager");  // Collection name is "Products"
            }
        }
    }
}