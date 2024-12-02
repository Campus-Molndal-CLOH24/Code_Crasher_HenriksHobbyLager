using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using System.Collections.Generic;
using System.Linq;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Interfaces;

namespace HenriksHobbyLager.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new();

        public IEnumerable<Product> GetAll()
        {
            return _products;
        }

        public Product GetById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public void Add(Product entity)
        {
            _products.Add(entity);
        }

        public void Update(Product entity)
        {
            var product = GetById(entity.Id);
            if (product != null)
            {
                product.Name = entity.Name;
                product.Price = entity.Price;
                product.Stock = entity.Stock;
                product.Category = entity.Category;
                product.LastUpdated = entity.LastUpdated;
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

        public IEnumerable<Product> Search(Func<Product, bool> predicate)
        {
            return _products.Where(predicate);
        }
        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            return _products.Where(p => p.CategoryId == categoryId);
        }
    }
}
