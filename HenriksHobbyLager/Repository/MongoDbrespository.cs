using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using HenriksHobbyLager.Database;
using Microsoft.Extensions.Configuration;


namespace HenriksHobbyLager.Repository;

public class MongoDbRepository : IRepository<Product>
{
    
    private readonly IMongoCollection<Product> _productsCollection;
    
    // GOOD: Using directly as a param
    public MongoDbRepository(IConfiguration configuration)
    {
        
        var client = new MongoClient(configuration.GetConnectionString("MongoDb"));
        var _mongoDbDatabase = client.GetDatabase("HenriksHobbyLager");
        _productsCollection = _mongoDbDatabase.GetCollection<Product>("Products");
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        
        return await _productsCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        
        return await _productsCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddAsync(Product entity)
    {
        await _productsCollection.InsertOneAsync(entity);
    }
    public async Task UpdateAsync(Product entity)
    {
        await _productsCollection.ReplaceOneAsync(p => p.Id == entity.Id, entity);
    }
    public async Task DeleteAsync(int id)
    {
        await _productsCollection.DeleteOneAsync(p => p.Id == id);
    }
    public async Task<IEnumerable<Product>> SearchAsync(Func<Product, bool> predicate)
    {
        var allProducts = await _productsCollection.Find(_ => true).ToListAsync(); //get allproduct
        return allProducts.Where(predicate); //filter based on predicate
    }

}

