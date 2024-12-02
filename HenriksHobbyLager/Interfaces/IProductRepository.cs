using HenriksHobbyLager.Models;
using System.Collections.Generic;

namespace HenriksHobbyLager.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll(); // Hämta alla produkter
        Product? GetById(int id);      // Hämta produkt via ID
        void Add(Product product);    // Lägg till en ny produkt
        void Update(Product product); // Uppdatera en produkt
        void Delete(int id);          // Ta bort en produkt via ID
        IEnumerable<Product> GetProductsByCategory(int categoryId); // Hämta produkter baserat på kategori

        IEnumerable<Product> Search(Func<Product, bool> predicate);
    }
}
