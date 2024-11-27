namespace HenriksHobbyLager.Interfaces;

public interface IProductFacade
{
    IEnumerable<Product> GetAllProducts();
    Product GetProduct(int id);
    void CreateProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(int id);
    IEnumerable<Product> SearchProducts(string searchTerm);
}

// this is c# 10 syntax recommended by the Cursor AI , it so weried because when i am using visual studio is  not told me to do like this.   
