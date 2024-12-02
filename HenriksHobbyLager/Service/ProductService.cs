using HenriksHobbyLager.Models;
using HenriksHobbyLager.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HenriksHobbyLager.Services
{
    public class ProductService
    {
        private readonly ProductRepository _repository;

        public ProductService(ProductRepository repository)
        {
            _repository = repository;
        }

        // Get all products
        public void DisplayAllProducts()
        {
            var products = _repository.GetAll();
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

        // Add a new product
        public void AddProduct(Product product)
        {
            if (product == null)
            {
                Console.WriteLine("Product cannot be null.");
                return;
            }

            _repository.Add(product);
            Console.WriteLine($"Product '{product.Name}' added successfully.");
        }

        // Update an existing product
        public void UpdateProduct(Product updatedProduct)
        {
            var existingProduct = _repository.GetById(updatedProduct.Id);
            if (existingProduct == null)
            {
                Console.WriteLine($"Product with ID {updatedProduct.Id} not found.");
                return;
            }

            _repository.Update(updatedProduct);
            Console.WriteLine($"Product '{updatedProduct.Name}' updated successfully.");
        }

        public void DeleteProduct(int id)
        {
            // First, retrieve the product by ID to ensure it exists
            var product = _repository.GetById(id);
            if (product == null)
            {
                Console.WriteLine($"Product with ID {id} not found.");
                return;
            }

            // Call the Delete method with the ID
            _repository.Delete(id);

            Console.WriteLine($"Product with ID {id} deleted successfully.");
        }

        // Search for products using a predicate
        public void SearchProducts(Func<Product, bool> predicate)
        {
            var results = _repository.Search(predicate);
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
