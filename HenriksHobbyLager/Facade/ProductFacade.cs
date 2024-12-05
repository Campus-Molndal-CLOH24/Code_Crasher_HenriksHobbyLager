using HenriksHobbyLager.Interfaces;
using System.Collections.Generic;

namespace HenriksHobbyLager.Facade
{
    public class ProductFacade<T>(IProductRepository<T> repository) : IProductFacade<T>
    {
        private readonly IProductRepository<T> _repository = repository;

        public IEnumerable<T> GetAllProducts()
        {
            return _repository.GetAll();
        }
        public T GetProductById(string id)
        {
            var product = _repository.GetById(id);

            if (product == null)
            {
                throw new InvalidOperationException($"Ingen produkt hittades med ID {id}.");
            }

            return product;
        }



        public void CreateProduct(T product)
        {
            _repository.Create(product);
        }

        public void UpdateProduct(T product)
        {
            _repository.Update(product);
        }

        public void DeleteProduct(string id)
        {
            _repository.Delete(id);
        }
    }
}
