using System;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
namespace HenriksHobbyLager.Facade;

using HenriksHobbyLager.Repositories;
using MongoDB.Bson;

public class ProductFacade : IProductFacade
{
    private readonly IRepository<Product> _productRepository; //for sql 
    public ProductFacade(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetProductAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new ArgumentException("Hitta inte produkt");
            }
            return product;
        }

        public async Task CreateProductAsync(Product product)
        {
            
            await _productRepository.AddAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {

            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }
// seach iteam by name(i created it to inteact with user)
        public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
        {
            return await _productRepository.SearchAsync(p => p.Name != null && p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }
    

}
