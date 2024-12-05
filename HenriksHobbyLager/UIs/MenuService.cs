using System;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Service;
using HenriksHobbyLager;

namespace HenriksHobbyLager.UIs

{



    public class MenuService<T>(ProductService<T> productService) where T : class, new()
    {
        private readonly ProductService<T> _productService = productService;

        public void DisplayMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("******** Henriks Hobby Lager ********");
                Console.WriteLine("1. Visa alla produkter");
                Console.WriteLine("2. Lägg till en produkt");
                Console.WriteLine("3. Uppdatera en produkt");
                Console.WriteLine("4. Ta bort en produkt");
                Console.WriteLine("5. Hämta produkter per kategori");
                Console.WriteLine("6. Avsluta");
                Console.WriteLine("****************************************");
                Console.Write("Välj ett alternativ: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        _productService.DisplayAllProducts();
                        break;

                    case "2":
                        AddProduct();
                        break;

                    case "3":
                        UpdateProduct();
                        break;

                    case "4":
                        DeleteProduct();
                        break;

                    case "5":
                        SearchByCategory();
                        break;

                    case "6":
                        Console.WriteLine("Avslutar programmet...");
                        return;

                    default:
                        Console.WriteLine("Ogiltigt val! Försök igen.");
                        break;
                }

                Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
            }
        }

        private void AddProduct()
        {
            Console.WriteLine("\nLägg till en ny produkt:");

            // Dynamisk skapelse av produktobjekt baserat på typen T
            T product = new T();

            // Sätt gemensamma egenskaper för både MongoDB och SQLite-produkter
            if (product is IProductBase commonProduct)
            {
                Console.Write("Ange namn: ");
                commonProduct.Name = Console.ReadLine() ?? string.Empty;

                Console.Write("Ange pris: ");
                commonProduct.Price = decimal.TryParse(Console.ReadLine(), out var price) ? price : 0;

                Console.Write("Ange lagerantal: ");
                commonProduct.Stock = int.TryParse(Console.ReadLine(), out var stock) ? stock : 0;

                if (commonProduct is ProductMongo mongoProduct)
                {
                    Console.Write("Ange kategori-ID (för MongoDB): ");
                    mongoProduct.CategoryId = int.TryParse(Console.ReadLine(), out var categoryId) ? categoryId : 0;

                    _productService.AddProduct(product);
                }
                else if (commonProduct is ProductSQLite sqliteProduct)
                {
                    Console.Write("Ange kategori-ID (för SQLite): ");
                    sqliteProduct.CategoryId = int.TryParse(Console.ReadLine(), out var categoryId) ? categoryId : 0;

                    _productService.AddProduct(product);
                }

                Console.WriteLine("Produkten lades till framgångsrikt!");
            }
            else
            {
                Console.WriteLine("Produkten kunde inte skapas. Typen stöds inte.");
            }
        }

        private void UpdateProduct()
        {
            Console.Write("\nAnge produkt-ID att uppdatera: ");
            var id = Console.ReadLine();

            Console.WriteLine("\nAnge nya värden för produkten:");
            var product = new T();

            if (product is ProductMongo mongoProduct)
            {
                mongoProduct.Id = id;
                Console.Write("Ange namn: ");
                mongoProduct.Name = Console.ReadLine() ?? string.Empty;

                Console.Write("Ange pris: ");
                mongoProduct.Price = decimal.TryParse(Console.ReadLine(), out var price) ? price : 0;

                Console.Write("Ange lagerantal: ");
                mongoProduct.Stock = int.TryParse(Console.ReadLine(), out var stock) ? stock : 0;

                _productService.UpdateProduct(product as T);
            }
            else if (product is ProductSQLite sqliteProduct)
            {
                sqliteProduct.Id = int.TryParse(id, out var intId) ? intId : 0;
                Console.Write("Ange namn: ");
                sqliteProduct.Name = Console.ReadLine() ?? string.Empty;

                Console.Write("Ange pris: ");
                sqliteProduct.Price = decimal.TryParse(Console.ReadLine(), out var price) ? price : 0;

                Console.Write("Ange lagerantal: ");
                sqliteProduct.Stock = int.TryParse(Console.ReadLine(), out var stock) ? stock : 0;

                _productService.UpdateProduct(product as T);
            }

            Console.WriteLine("Produkten uppdaterades framgångsrikt!");
        }

        private void DeleteProduct()
        {
            Console.Write("\nAnge produkt-ID att ta bort: ");
            var id = Console.ReadLine();

            if (string.IsNullOrEmpty(id))
            {
                Console.WriteLine("Ogiltigt ID!");
                return;
            }

            try
            {
                _productService.DeleteProduct(id);
                Console.WriteLine("Produkten togs bort framgångsrikt!");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void SearchByCategory()
        {
            Console.Write("\nAnge kategori-ID för att söka efter produkter: ");
            if (int.TryParse(Console.ReadLine(), out int categoryId))
            {
                _productService.SearchByCategory(categoryId);
            }
            else
            {
                Console.WriteLine("Ogiltigt kategori-ID!");
            }
        }
    }
}