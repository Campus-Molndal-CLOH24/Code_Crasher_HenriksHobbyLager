using System;
using System.Collections.Generic;
using HenriksHobbyLager.Database;
using Microsoft.EntityFrameworkCore;
using HenriksHobbyLager.Models;
using System.IO;
using System.Globalization;

namespace HenriksHobbyLager.UIs;
static class ConsoleMenuHandler
{
    
    // create a new instance of SqliteDbcontext
    //It creates a connection to your SQLite database and act like a bridge between your code and the database.
    //also help with data management operations.
    private static readonly SqliteDbcontext _context = new SqliteDbcontext(
    new DbContextOptionsBuilder<SqliteDbcontext>()
        .UseSqlite("Data Source=ProductsHobbyLager.db")
        .Options);

    public static void ShowMainMenu()
    {
        while (true)
        {
            Console.Clear();  // Rensar skärmen så det ser proffsigt ut
            Console.WriteLine("=== Henriks HobbyLager™ 2.0 ===");
            Console.WriteLine("1. Visa alla produkter");
            Console.WriteLine("2. Lägg till produkt");
            Console.WriteLine("3. Uppdatera produkt");
            Console.WriteLine("4. Ta bort produkt");
            Console.WriteLine("5. Sök produkter");
            Console.WriteLine("6. Avsluta");  // Använd inte denna om du vill behålla datan!

            var choice = Console.ReadLine();

            // Switch är tydligen bättre än if-else enligt Google
            switch (choice)
            {
                case "1":
                    ShowAllProducts();
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
                    SearchProducts();
                    break;
                case "6":
                    return;  // OBS! All data försvinner om du väljer denna!
                default:
                    Console.WriteLine("Ogiltigt val! Är du säker på att du tryckte på rätt knapp?");
                    break;
            }

            Console.WriteLine("\nTryck på valfri tangent för att fortsätta... (helst inte ESC)");
            Console.ReadKey();
        }
    }

    private static void ShowAllProducts()
    {
        var products = _context.Product.ToList();
        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.Id}, Namn: {product.Name}, Pris: {product.Price}kr, Lager: {product.Stock}");
        }
    }
    // use error handling to controll user input
    private static void AddProduct()
    {
        try
        {
            Console.WriteLine("Ange produktnamn:");
            var name = Console.ReadLine();

            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Produktnamn kan inte vara tomt!");
                return;
            }

            decimal price = GetPriceFromUser(); // Use refactored method
            Console.WriteLine("Ange antal i lager:");

            if (int.TryParse(Console.ReadLine(), out int stock) && stock >= 0)
            {
                Console.WriteLine("Ange datum och tid för skapandet (yyyy-MM-dd):");
                DateTime createDate;
                if (DateTime.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, DateTimeStyles.None, out createDate))
                {
                    var product = new Product
                    {
                        Name = name,
                        Price = price,
                        Stock = stock,
                        Created = createDate,
                        LastUpdated = DateTime.UtcNow
                    };

                    _context.Product.Add(product);
                    _context.SaveChanges();
                    Console.WriteLine("Produkt tillagd!");
                }
                else
                {
                    Console.WriteLine("Ogiltigt antal i lager.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ett fel inträffade vid tillägg av produkt: {ex.Message}");
        }
    }

    private static decimal GetPriceFromUser()
    {
        Console.WriteLine("Ange pris:");
        while (true)
        {
            if (decimal.TryParse(Console.ReadLine(), out decimal price) && price > 0)
                return price;

            Console.WriteLine("Ogiltigt pris, försök igen.");
        }
    }

    //updated error handling 
    private static void UpdateProduct()
    {
        Console.WriteLine("Ange produkt-ID att uppdatera:");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var product = _context.Product.Find(id);
            if (product != null)
            {
                Console.WriteLine("Ange nytt namn (eller tryck Enter för att behålla nuvarande):");
                var name = Console.ReadLine();
                if (!string.IsNullOrEmpty(name))
                    product.Name = name;

                Console.WriteLine("Ange nytt pris (eller tryck Enter för att behålla nuvarande):");
                var priceInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(priceInput) && decimal.TryParse(priceInput, out decimal price))
                    product.Price = price;

                try
                {
                    _context.SaveChanges(); // Database operation to save changes
                    Console.WriteLine("Produkt uppdaterad!");
                }
                catch (Exception ex)
                {
                    // Catch any errors that occur during the save operation
                    Console.WriteLine($"Ett fel inträffade vid uppdatering av produkt: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Produkt hittades inte!");
            }
        }
    }

    private static void DeleteProduct()
    {
        Console.WriteLine("Ange produkt-ID att ta bort:");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var product = _context.Product.Find(id);
            if (product != null)
            {
                _context.Product.Remove(product);
                _context.SaveChanges();
                Console.WriteLine("Produkt borttagen!");
            }
            else
            {
                Console.WriteLine("Produkt hittades inte!");
            }
        }
    }

    private static void SearchProducts()
    {
        Console.WriteLine("Ange sökterm:");
        var searchTerm = Console.ReadLine()?.ToLower() ?? "";

        var products = _context.Product
            .Where(p => p.Name != null && p.Name.ToLower().Contains(searchTerm))
            .ToList();

        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.Id}, Namn: {product.Name}, Pris: {product.Price}kr, Lager: {product.Stock}");
        }
    }
}




