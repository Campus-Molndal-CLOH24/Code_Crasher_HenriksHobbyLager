using HenriksHobbyLager.Models;
using HenriksHobbyLager.Interfaces; // Import för IProductFacade
using HenriksHobbyLager.Facade;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HenriksHobbyLager.Services
{
    public class ProductService
    {
        private readonly IProductFacade _productFacade;

        // Konstruktorn injicerar IProductFacade istället för ProductRepository
        public ProductService(IProductFacade productFacade)
        {
            _productFacade = productFacade;
        }

        // Hämta alla produkter
        public void DisplayAllProducts()
        {
            var products = _productFacade.GetAllProducts();
            if (!products.Any())
            {
                Console.WriteLine("No products found.");
                return;
            }

            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price:C}, Stock: {product.Stock}");
            }
        }

        // Lägg till en ny produkt
        public void AddProduct(Product product)
        {
            if (product == null)
            {
                Console.WriteLine("Product cannot be null.");
                return;
            }

            _productFacade.CreateProduct(product);
            Console.WriteLine($"Product '{product.Name}' added successfully.");
        }

        // Uppdatera en befintlig produkt
        public void UpdateProduct(Product updatedProduct)
        {
            var existingProduct = _productFacade.GetProductById(updatedProduct.Id);
            if (existingProduct == null)
            {
                Console.WriteLine($"Product with ID {updatedProduct.Id} not found.");
                return;
            }

            _productFacade.UpdateProduct(updatedProduct);
            Console.WriteLine($"Product '{updatedProduct.Name}' updated successfully.");
        }

        // Ta bort en produkt
        public void DeleteProduct(int id)
        {
            // Först hämta produkten via ID för att säkerställa att den existerar
            var product = _productFacade.GetProductById(id);
            if (product == null)
            {
                Console.WriteLine($"Product with ID {id} not found.");
                return;
            }

            // Anropa Delete-metoden med ID:t
            _productFacade.DeleteProduct(id);
            Console.WriteLine($"Product with ID {id} deleted successfully.");
        }

        // Sök efter produkter med hjälp av en predikatfunktion
        public void SearchProducts(Func<Product, bool> predicate)
        {
            var results = _productFacade.SearchProducts(predicate);
            if (!results.Any())
            {
                Console.WriteLine("No products matched your search.");
                return;
            }

            Console.WriteLine("Search results:");
            foreach (var product in results)
            {
                Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price:C}");
            }
        }
    }
}

