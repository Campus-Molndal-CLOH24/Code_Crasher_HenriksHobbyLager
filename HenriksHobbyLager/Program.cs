using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.Sqlite;
using HenriksHobbyLager.Database;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Repositories;
using Microsoft.EntityFrameworkCore;
using System;



namespace HenriksHobbyLager
{
    static class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create the DbContext instance that connects to your SQLite database
            var options = new DbContextOptionsBuilder<SqliteDbcontext>()
                .UseSqlite("Data Source=ProductsHobbyLager.db") // Use your actual SQLite database connection string here
                .Options;
            // add product 
            using (var context = new SqliteDbcontext(options))
            {
                // Add some products if not already in the database
                if (!context.Product.Any())
                {
                    context.Product.AddRange(
                        new Product { Name = "SuperCar", Price = 1500, Stock = 20, Category = "Cars", Created = new DateTime(2024, 3, 15, 14, 30, 0, DateTimeKind.Utc), LastUpdated = DateTime.Now }

                    );
                    context.SaveChanges();
                }
            }
            // search product test 
            using (var context = new SqliteDbcontext(options))
            {
                // Step 2: Create the ProductRepository and add some test data if necessary
                var productRepository = new ProductRepository(context);

                // Step 3: Test SearchByName method with some search terms
                var searchTerm = "Sup"; // Searching for products with "Lap" in the name
                var result = productRepository.SearchByName(searchTerm);

                // Step 4: Output the search results to the console
                Console.WriteLine($"Search results for '{searchTerm}':");
                foreach (var product in result)
                {
                    Console.WriteLine($"Found: {product.Name}");
                }
            }
            using (var context = new SqliteDbcontext(options))
            {
                var products = context.Product.ToList();
                Console.WriteLine("All Products in Database:");
                foreach (var product in products)
                {
                    Console.WriteLine($"Product: {product.Name}, Price: {product.Price}, Stock: {product.Stock}");
                }
            }

            // Optional: Keep the console window open
            Console.ReadLine();
        }
    }
}




