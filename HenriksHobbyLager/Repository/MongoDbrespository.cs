using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using HenriksHobbyLager.Database;


namespace HenriksHobbyLager.Repository;

public class MongoDbRepository : IRepository<Product>
{
    private readonly IMongoCollection<Product> _productsCollections;
    
    //constructor
    public MongoDbRepository(MongoDbContext mongoDbContext)
    {
        if (mongoDbContext.Products == null)
            throw new ArgumentException("Products collection is null");
        
        _productsCollections = mongoDbContext.Products;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _productsCollections.Find(_ => true).ToListAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _productsCollections.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddAsync(Product entity)
    {
        await _productsCollections.InsertOneAsync(entity);
    }
    public async Task UpdateAsync(Product entity)
    {
        await _productsCollections.ReplaceOneAsync(p => p.Id == entity.Id, entity);
    }
    public async Task DeleteAsync(int id)
    {
        await _productsCollections.DeleteOneAsync(p => p.Id == id);
    }
    public async Task<IEnumerable<Product>> SearchAsync(Func<Product, bool> predicate)
    {
        return await _productsCollections.Find(x => predicate(x)).ToListAsync();
    }

}

