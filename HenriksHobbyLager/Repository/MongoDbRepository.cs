using HenriksHobbyLager.Database;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using HenriksHobbyLager.Service;
using MongoDB.Driver;

namespace HenriksHobbyLager.Repository
{
    public class MongoDBRepository(MongoDbContext context) : IProductRepository
    {
        private readonly IMongoCollection<Product> _products = context.Products ?? throw new ArgumentNullException(nameof(context));

        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.CategoryId, categoryId);
            return _products.Find(filter).ToList();
        }

        public IEnumerable<Product> GetAll()
        {
            // Logik för att hämta alla produkter från MongoDB
            List<Product> value = [];
            List<Product> products = value;
            return products;
        }

        public Product? GetById(int id)
        {
            // Logik för att hämta en produkt med ID från MongoDB
            return null;
        }

        public void Create(Product product)
        {
            // Logik för att skapa en ny produkt i MongoDB
        }

        public void Update(Product product)
        {
            // Logik för att uppdatera en produkt i MongoDB
        }

        public void Delete(int id)
        {
            // Logik för att ta bort en produkt i MongoDB
        }

        public IEnumerable<Product> Search(string searchText, bool predicate)
        {
            // Logik för att söka efter produkter i MongoDB
            return [];
        }
    }
}
