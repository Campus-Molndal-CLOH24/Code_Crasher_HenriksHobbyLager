using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Service;
using HenriksHobbyLager;
using System.Collections.Generic;
using System.Linq;

namespace HenriksHobbyLager.Repository
{
    // Denna klass implementerar IProductRepository och ansvarar för CRUD-operationer på en lista av produkter, enligt Repository-mönstret.
    public class ProductRepository : IProductRepository
    {
        // Den privata listan '_products' fungerar som en temporär lagringslösning för produktdata i minnet.
        private readonly List<Product> _products = new();

        // Metoden 'GetAll' returnerar alla produkter i listan.
        public IEnumerable<Product> GetAll()
        {
            return _products;
        }

        // Metoden 'GetById' söker efter en produkt baserat på id och returnerar den, eller null om den inte finns.
        public Product? GetById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        // Metoden 'Create' lägger till en ny produkt i listan.
        public void Create(Product product)
        {
            _products.Add(product);
        }

        // Metoden 'Update' uppdaterar informationen för en befintlig produkt, om den finns i listan.
        public void Update(Product product)
        {
            var existingProduct = GetById(product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Stock = product.Stock;
            }
        }

        // Metoden 'Delete' tar bort en produkt baserat på dess id om produkten finns i listan.
        public void Delete(int id)
        {
            var product = GetById(id);
            if (product != null)
            {
                _products.Remove(product);
            }
        }

        // Metoden 'GetProductsByCategory' returnerar alla produkter som tillhör en specifik kategori, baserat på categoryId.
        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            return _products.Where(p => p.CategoryId == categoryId).ToList();
        }

        // Metoden 'Search' söker efter produkter vars namn innehåller den givna söktexten.
        public IEnumerable<Product> Search(string searchText, bool predicate)
        {
            return _products.Where(p => p.Name.Contains(searchText));
        }
    }
}
