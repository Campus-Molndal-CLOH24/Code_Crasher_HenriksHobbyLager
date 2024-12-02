using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using System.Collections.Generic;

namespace HenriksHobbyLager.Facade
{
    public class ProductFacade(IProductRepository repository) : IProductFacade
    {
        private readonly IProductRepository _repository = repository;

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
            _repository.Create(product);
        }

        public void UpdateProduct(Product product)
        {
            _repository.Update(product);
        }

        public void DeleteProduct(int id)
        {
            _repository.Delete(id);
        }

        public IEnumerable<Product> SearchProducts(string searchText, bool predicate)
        {
            return _repository.Search(searchText, predicate);
        }
        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            return _repository.GetProductsByCategory(categoryId);
        }
    }
}
