using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using HenriksHobbyLager.Database;


namespace HenriksHobbyLager.Repository;

public class MongoDbRepository : IRepository<Product>
{
    private readonly IMongoCollection<Product> _productsCollections;
    
    
    public MongoDbRepository(MongoDbContext mongoDbContext)
    {
        _productsCollections = mongoDbContext.Products;
        
    }

    public IEnumerable<Product> GetAll()
    {
        return _productsCollections.Find(_ => true).ToList();
    }

    public Product GetById(int id)
    {
        return _productsCollections.Find(p => p.Id == id).FirstOrDefault();
    }

    public void Add(Product entity)
    {
        _productsCollections.InsertOne(entity);
    }
    public void Update(Product entity)
    {
        _productsCollections.ReplaceOne(p => p.Id == entity.Id, entity);
    }
    public void Delete(int id)
    {
        _productsCollections.DeleteOne(p => p.Id == id);
    }
    public IEnumerable<Product> Search(Func<Product, bool> predicate)
    {
        return _productsCollections.AsQueryable().Where(predicate).ToList(); //convert the MongoDB collection to an IQueryable
    }

}

