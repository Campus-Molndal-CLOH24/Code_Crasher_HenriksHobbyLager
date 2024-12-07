using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using HenriksHobbyLager.Database;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace HenriksHobbyLager.Repository;

public class MongoDbRepository : IRepository<Product>
{

    private readonly IMongoCollection<Product> _productsCollection;
    



    public MongoDbRepository(MongoDbContext context)
    {

        var mongoDbConnectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING");
        var client = new MongoClient(mongoDbConnectionString);
        var database = client.GetDatabase("productdb"); // Use your database name
        _productsCollection = database.GetCollection<Product>("HenriksHobbyLager"); // Use your collection name
       
    }

    //Crud operation.
    public async Task<IEnumerable<Product>> GetAllAsync()
    {

        return await _productsCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {

        var filter = Builders<Product>.Filter.Eq("MongoId", id);
        return await _productsCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task AddAsync(Product product)
    {
        product.MongoId = GetNextMongoId(); // Generate unique int for MongoId
        await _productsCollection.InsertOneAsync(product);
        
    }
    
        private int GetNextMongoId()
        {
            // Check for existing IDs in the collection to avoid duplicates
            var existingIds = _productsCollection.AsQueryable().Select(p => p.MongoId).ToList();
            int newId = 1; // Start from 1 or any other base value

            while (existingIds.Contains(newId))
            {
                newId++; // Increment until a unique ID is found
            }

            return newId; // Return the unique ID
        }
    public async Task UpdateAsync(Product product)
    {
        var filter = Builders<Product>.Filter.Eq("MongoId", product.Id); // Ensure the product has an _id field to use in the filter
        await _productsCollection.ReplaceOneAsync(filter, product);
    }
    public async Task DeleteAsync(int id)
    {
        var filter = Builders<Product>.Filter.Eq("MongoId", id);
        await _productsCollection.DeleteOneAsync(filter);
    }
    public async Task<IEnumerable<Product>> SearchAsync(Func<Product, bool> predicate)
    {
        var allProducts = await _productsCollection.Find(_ => true).ToListAsync(); //get allproduct
        return allProducts.Where(predicate); //filter based on predicate
    }

}

