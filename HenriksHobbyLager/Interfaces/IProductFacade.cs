using System.Collections.Generic;

namespace HenriksHobbyLager.Interfaces
{
    public interface IProductFacade<T>
    {
        IEnumerable<T> GetAllProducts();
        T GetProductById(string id);
        void CreateProduct(T product);
        void UpdateProduct(T product);
        void DeleteProduct(string id);
    }
}
