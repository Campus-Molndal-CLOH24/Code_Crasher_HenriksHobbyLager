using System.Collections.Generic;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Database;
using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;

namespace HenriksHobbyLager.Facade
{
    public class ProductFacade
    {
        private readonly IProductRepository _repository;

        public ProductFacade(IProductRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _repository.GetAll();
        }

        public Product? GetProductById(int id)
        {
            return _repository.GetById(id);
        }

        public void CreateProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _repository.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _repository.Update(product);
        }

        public void DeleteProduct(int id)
        {
            _repository.Delete(id);
        }

        public IEnumerable<Product> SearchProducts(string searchTerm, Product p)
        {
            return _repository.GetAll()
                .Where(predicate: p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }
    }
}
