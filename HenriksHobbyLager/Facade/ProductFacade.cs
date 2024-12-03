using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using System.Collections.Generic;
using HenriksHobbyLager.Service;

namespace HenriksHobbyLager.Facade
{
    // Denna klass använder Facade-mönstret för att ge ett enklare gränssnitt till produktrelaterade funktioner och interagerar med repository-lagret.
    public class ProductFacade(IProductRepository repository) : IProductFacade
    {
        // Den privata readonly-fältet '_repository' används för att lagra beroendet av IProductRepository och säkerställa att det inte kan ändras efter initialiseringen.
        private readonly IProductRepository _repository = repository;

        // Metoden 'GetAllProducts' används för att hämta alla produkter från databasen via repository-lagret.
        public IEnumerable<Product> GetAllProducts()
        {
            return _repository.GetAll();
        }

        // Metoden 'GetProductById' hämtar en specifik produkt baserat på ett unikt id. Returnerar null om produkten inte finns.
        public Product? GetProductById(int id)
        {
            return _repository.GetById(id);
        }

        // Metoden 'CreateProduct' används för att skapa en ny produkt och lägga till den i databasen.
        public void CreateProduct(Product product)
        {
            _repository.Create(product);
        }

        // Metoden 'UpdateProduct' uppdaterar informationen för en befintlig produkt.
        public void UpdateProduct(Product product)
        {
            _repository.Update(product);
        }

        // Metoden 'DeleteProduct' tar bort en produkt baserat på dess id.
        public void DeleteProduct(int id)
        {
            _repository.Delete(id);
        }

        // Metoden 'SearchProducts' används för att söka efter produkter som matchar ett sökord eller villkor. Parametern 'predicate' kan användas för att justera sökningen.
        public IEnumerable<Product> SearchProducts(string searchText, bool predicate)
        {
            return _repository.Search(searchText, predicate);
        }

        // Metoden 'GetProductsByCategory' hämtar alla produkter som tillhör en specifik kategori, baserat på categoryId.
        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            return _repository.GetProductsByCategory(categoryId);
        }
    }
}

