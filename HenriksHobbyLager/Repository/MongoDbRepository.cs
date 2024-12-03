using HenriksHobbyLager.Database;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using HenriksHobbyLager.Service;
using MongoDB.Driver;

namespace HenriksHobbyLager.Repository
{
    public class MongoDBRepository(IMongoCollection<Product> products) : IProductRepository
    {
        private readonly IMongoCollection<Product> _products = products ?? throw new ArgumentNullException(nameof(products));

        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.CategoryId, categoryId);
            return _products.Find(filter).ToList(); // Returnera alla produkter som matchar kategoriId
        }

        public IEnumerable<Product> GetAll()
        {
            return _products.Find(_ => true).ToList(); // Hämta alla dokument i collectionen
        }

        public Product? GetById(int id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            return _products.Find(filter).FirstOrDefault(); // Hämta produkten med det angivna id:et
        }

        public void Create(Product product)
        {
            _products.InsertOne(product); // Lägg till produkten i MongoDB
        }

        public void Update(Product product)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);
            _products.ReplaceOne(filter, product); // Uppdatera produkten med motsvarande id
        }

        public void Delete(int id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            _products.DeleteOne(filter); // Ta bort produkten med motsvarande id
        }

        public IEnumerable<Product> Search(string searchText, bool predicate)
        {
            var filter = Builders<Product>.Filter.Regex(p => p.Name, new MongoDB.Bson.BsonRegularExpression(searchText, predicate ? "i" : ""));
            return _products.Find(filter).ToList(); // Returnera alla produkter som matchar söktexten
        }
    }
}
