using HenriksHobbyLager.Models;


namespace HenriksHobbyLager.Interfaces
{
    public interface IProductFacade
    {
        IEnumerable<Product> GetAllProducts();
        Product? GetProductById(int id);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        IEnumerable<Product> SearchProducts(string searchText, bool predicate);
        IEnumerable<Product> GetProductsByCategory(int categoryId);
    }
}
