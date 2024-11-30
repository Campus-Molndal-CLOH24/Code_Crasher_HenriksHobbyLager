using System.Collections.Generic;
using HenriksHobbyLager.Database;
using Microsoft.EntityFrameworkCore;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Facade;
using System.IO;
using System.Globalization;
using MongoDB.Driver;


namespace HenriksHobbyLager.UIs;
class ConsoleMenuHandler
{
    private readonly IProductFacade _facade;
    private readonly IRepository<Product> _repository;
    
    
    
    public ConsoleMenuHandler(IProductFacade facade, DatabaseFactory databaseFactory)
    {
        // Initialize the repository based on selected database
        var _databaseMenu = new DatabaseMenu(databaseFactory);
        _repository = _databaseMenu.GetSelectedRepository();

        // Initialize the facade with the selected repository
        _facade = facade;
    }

    public void ShowMainMenu()
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
            WaitForUserInput();

        }
    }
    private static void WaitForUserInput()
    {
        Console.WriteLine("\nTryck på valfri tangent för att fortsätta... (helst inte ESC)");
        Console.ReadKey();
    }

    private void ShowAllProducts()
    {
        var products = _facade.GetAllProducts();
        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.Id}, Namn: {product.Name}, Pris: {product.Price}kr, Lager: {product.Stock}");
        }
    }
    // use error handling to controll user input
    private void AddProduct()
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

                    _repository.Add(product);
                    
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
    private void UpdateProduct()
    {
        Console.WriteLine("Ange produkt-ID att uppdatera:");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var product = _repository.GetById(id);
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
                    _repository.Update(product); // change from savechanges to update
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

    private void DeleteProduct()
    {
        Console.WriteLine("Ange produkt-ID att ta bort:");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var product = _repository.GetById(id);
            if (product != null)
            {
                _repository.Delete(id);
                Console.WriteLine("Produkt borttagen!");
            }
            else
            {
                Console.WriteLine("Produkt hittades inte!");
            }
        }
    }

    private void SearchProducts()
    {
        Console.WriteLine("Ange sökterm:");
        var searchTerm = Console.ReadLine()?.ToLower() ?? "";

        var products = _facade.GetAllProducts()
            .Where(p => p.Name != null && p.Name.ToLower().Contains(searchTerm))
            .ToList();

        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.Id}, Namn: {product.Name}, Pris: {product.Price}kr, Lager: {product.Stock}");
        }
    }
}




