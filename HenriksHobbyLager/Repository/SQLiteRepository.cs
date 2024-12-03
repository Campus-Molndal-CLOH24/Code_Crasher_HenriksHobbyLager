using System;
using System.Collections.Generic;
using System.Linq;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Database;

namespace HenriksHobbyLager.Repository
{
    public class SQLiteRepository(SqliteDbContext context) : IProductRepository
    {
        private readonly SqliteDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        // Hämta alla produkter
        public IEnumerable<Product> GetAll()
        {
            return [.. _context.Products];
        }

        // Hämta en produkt baserat på ID
        public Product? GetById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        // Lägg till en ny produkt
        public void Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        // Uppdatera en befintlig produkt
        public void Update(Product updatedProduct)
        {
            var existingProduct = _context.Products.Find(updatedProduct.Id);
            if (existingProduct != null)
            {
                _context.Entry(existingProduct).CurrentValues.SetValues(updatedProduct);
                _context.SaveChanges();
            }
        }

        // Ta bort en produkt
        public void Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        // Hämta produkter baserat på kategori
        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            return [.. _context.Products.Where(p => p.CategoryId == categoryId)];
        }
  
        public IEnumerable<Product> Search(string searchText, bool predicate)
        {
            throw new NotImplementedException();
        }
    }
}
