using System;
namespace HenriksHobbyLager.UIs;
using System.Collections.Generic;
using HenriksHobbyLager.Database;
using Microsoft.EntityFrameworkCore;

public static class MenuService
{
    // Add database context as a static field
    private static readonly SqliteDbcontext _context = new SqliteDbcontext();

    static void Main(string[] args)
    {
        // Huvudloopen - Stäng inte av programmet, då försvinner allt!
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

    private static void AddProduct()
    {
        Console.WriteLine("Ange produktnamn:");
        var name = Console.ReadLine();

        Console.WriteLine("Ange pris:");
        if (decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            Console.WriteLine("Ange antal i lager:");
            if (int.TryParse(Console.ReadLine(), out int stock))
            {
                var product = new Product
                {
                    Name = name,
                    Price = price,
                    Stock = stock
                };

                _context.Product.Add(product);
                _context.SaveChanges();
                Console.WriteLine("Produkt tillagd!");
            }
        }
    }

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

                _context.SaveChanges();
                Console.WriteLine("Produkt uppdaterad!");
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
            .Where(p => p.Name.ToLower().Contains(searchTerm))
            .ToList();

        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.Id}, Namn: {product.Name}, Pris: {product.Price}kr, Lager: {product.Stock}");
        }
    }
}
