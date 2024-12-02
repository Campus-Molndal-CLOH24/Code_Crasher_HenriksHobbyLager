using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using System.Collections.Generic;
using System.Linq;

namespace HenriksHobbyLager.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new();

        public IEnumerable<Product> GetAll()
        {
            return _products;
        }

        public Product? GetById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public void Create(Product product)
        {
            _products.Add(product);
        }

        public void Update(Product product)
        {
            var existingProduct = GetById(product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Stock = product.Stock;
            }
        }

        public void Delete(int id)
        {
            var product = GetById(id);
            if (product != null)
            {
                _products.Remove(product);
            }
        }

        public IEnumerable<Product> GetProductsByCategory(Category category)
        {
            ArgumentNullException.ThrowIfNull(category);

            // Antar att p.Category är en string och category.Name används för jämförelse
            return _products.Where(p => p.Category == category.Name);
        }


        public IEnumerable<Product> Search(string searchText, bool predicate)
        {
            return _products.Where(p => p.Name.Contains(searchText));
        }

        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
