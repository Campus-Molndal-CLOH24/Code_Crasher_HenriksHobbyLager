using System.Collections.Generic;

namespace HenriksHobbyLager.Interfaces
{
    public interface IProductRepository<T>
    {
        IEnumerable<T> GetAll();
        T? GetById(string id);
        void Create(T product);
        void Update(T product);
        void Delete(string id);
        IEnumerable<T> Search(string searchText, bool predicate); // Lägg till denna metod
        IEnumerable<T> GetProductsByCategory(int categoryId);

    }
}
