namespace HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;

public interface IProductFacade
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product> GetProductAsync(int id);
    Task CreateProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(int id);
    Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm);
}

