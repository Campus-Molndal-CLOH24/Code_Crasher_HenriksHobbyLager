using System.Collections.Generic;
using System.Linq;
using HenriksHobbyLager.Facade;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Interfaces;
using MongoDB.Driver;
using HenriksHobbyLager.Database;

namespace HenriksHobbyLager.Repository;
public class MongoDBRepository : IProductRepository
{
    private readonly MongoDbContext _context;

    public MongoDBRepository(MongoDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Product> GetAll()
    {
        return _context.Products.Find(_ => true).ToList();
    }

    public Product? GetById(int id)
    {
        return _context.Products.Find(p => p.Id == id).FirstOrDefault();
    }

    public void Add(Product product)
    {
        _context.Products.InsertOne(product);
    }

    public void Update(Product product)
    {
        _context.Products.ReplaceOne(p => p.Id == product.Id, product);
    }

    public void Delete(int id)
    {
        _context.Products.DeleteOne(p => p.Id == id);
    }

    public IEnumerable<Product> GetProductsByCategory(int categoryId)
    {
        return _context.Products.Find(p => p.CategoryId == categoryId).ToList();
    }
    
    public IEnumerable<Product> Search(Func<Product, bool> predicate)
    {
        return _products.AsQueryable().Where(predicate).ToList();
    }
}
