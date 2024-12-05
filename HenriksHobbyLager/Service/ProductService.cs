using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HenriksHobbyLager.Service
{
    public class ProductService<T>(IProductRepository<T> productRepository) where T : class
    {
        private readonly IProductRepository<T> _productRepository = productRepository;

        // Visa alla produkter
        public void DisplayAllProducts()
        {
            var products = _productRepository.GetAll();
            if (!products.Any())
            {
                Console.WriteLine("Inga produkter hittades.");
                return;
            }

            foreach (var product in products)
            {
                Console.WriteLine(product); // Detta kommer nu att anropa ToString()
            }
        }

        // Lägg till en produkt
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

        // Uppdatera en produkt
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

        // Sök produkter per kategori
        public void SearchByCategory(int categoryId)
        {
            // Kontrollera om modellen har en kategori (tillämpligt för SQLite och Mongo)
            if (typeof(T) == typeof(ProductSQLite))
            {
                var sqliteRepo = _productRepository as IProductRepository<ProductSQLite>;
                var products = sqliteRepo?.GetProductsByCategory(categoryId);

                if (products == null || !products.Any())
                {
                    Console.WriteLine("Inga produkter hittades för kategorin.");
                    return;
                }

                foreach (var product in products)
                {
                    Console.WriteLine(product);
                }
            }
            else
            {
                Console.WriteLine("Sökning efter kategori stöds endast för SQLite.");
            }
        }
    }
}
