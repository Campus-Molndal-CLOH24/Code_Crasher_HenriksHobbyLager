using HenriksHobbyLager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq; // Add this using directive

namespace HenriksHobbyLager.Service
{
    public class ProductService<T>
    {
        private readonly IProductRepository<T> _productRepository;

        public ProductService(IProductRepository<T> productRepository)
        {
            _productRepository = productRepository;
        }

        // Visa alla produkter
        public void DisplayAllProducts()
        {
            var products = _productRepository.GetAll();
            if (products == null || !products.Any())
            {
                Console.WriteLine("Inga produkter hittades.");
                return;
            }

            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
        }

        // Lägg till en ny produkt
        public void AddProduct(T product)
        {
            if (product == null)
            {
                Console.WriteLine("Produkten kan inte vara null.");
                return;
            }

            _productRepository.Create(product);
            Console.WriteLine("Produkten lades till framgångsrikt.");
        }

        // Uppdatera en befintlig produkt
        public void UpdateProduct(T product)
        {
            if (product == null)
            {
                Console.WriteLine("Produkten kan inte vara null.");
                return;
            }

            _productRepository.Update(product);
            Console.WriteLine("Produkten uppdaterades framgångsrikt.");
        }

        // Ta bort en produkt
        public void DeleteProduct(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Console.WriteLine("ID kan inte vara null eller tomt.");
                return;
            }

            _productRepository.Delete(id);
            Console.WriteLine("Produkten togs bort framgångsrikt.");
        }

        // Sök efter produkter
        public void SearchProducts(string searchText, bool predicate)
        {
            // Assuming the search functionality is not implemented in the repository
            var products = _productRepository.GetAll();
            var results = products.Where(p => predicate ? p.ToString().Contains(searchText) : !p.ToString().Contains(searchText));
            if (results == null || !results.Any())
            {
                Console.WriteLine("Inga produkter matchade din sökning.");
                return;
            }

            foreach (var product in results)
            {
                Console.WriteLine(product);
            }
        }
    }
}
