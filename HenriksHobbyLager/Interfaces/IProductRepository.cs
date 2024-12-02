using HenriksHobbyLager.Models;
using System.Collections.Generic;
using HenriksHobbyLager.Service;
namespace HenriksHobbyLager.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll(); // Hämta alla produkter
        Product? GetById(int id);      // Hämta en produkt med ID
        void Create(Product product); // Skapa en ny produkt
        void Update(Product product); // Uppdatera en produkt
        void Delete(int id);          // Ta bort en produkt med ID
        IEnumerable<Product> Search(string searchText, bool predicate); // Sök efter produkter
        IEnumerable<Product> GetProductsByCategory(int categoryId);
    }
}
