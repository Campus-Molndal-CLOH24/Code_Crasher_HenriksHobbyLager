using System;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Database;
using HenriksHobbyLager.Repository;
using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;

namespace HenriksHobbyLager;
public class ProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

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

    private void ShowAllProducts()
    {
        Console.WriteLine("\nAlla produkter:");
        var products = _repository.GetAll();

        if (!products.Any())
        {
            Console.WriteLine("Inga produkter hittades.");
            return;
        }

        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.Id}, Namn: {product.Name}, Pris: {product.Price}, Lager: {product.Stock}");
        }
    }

    private void AddProduct()
    {
        Console.Write("\nAnge produktnamn: ");
        var name = Console.ReadLine();

        Console.Write("Ange kategori: ");
        var category = Console.ReadLine();

        Console.Write("Ange pris: ");
        if (!decimal.TryParse(Console.ReadLine(), out var price))
        {
            Console.WriteLine("Ogiltigt pris!");
            return;
        }

        Console.Write("Ange lagerantal: ");
        if (!int.TryParse(Console.ReadLine(), out var stock))
        {
            Console.WriteLine("Ogiltigt lagerantal!");
            return;
        }

        var product = new Product
        {
            Name = name ?? "Okänd",
            Category = category ?? "Okänd",
            Price = price,
            Stock = stock
        };

        _repository.Add(product);
        Console.WriteLine($"Produkten '{name}' har lagts till.");
    }

    private void UpdateProduct()
    {
        Console.Write("\nAnge ID för produkten du vill uppdatera: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Ogiltigt ID!");
            return;
        }

        var existingProduct = _repository.GetById(id);
        if (existingProduct == null)
        {
            Console.WriteLine($"Ingen produkt hittades med ID {id}.");
            return;
        }

        Console.Write("Ange nytt namn (lämna tomt för att behålla nuvarande): ");
        var name = Console.ReadLine();
        Console.Write("Ange ny kategori (lämna tomt för att behålla nuvarande): ");
        var category = Console.ReadLine();

        Console.Write("Ange nytt pris (lämna tomt för att behålla nuvarande): ");
        var priceInput = Console.ReadLine();
        var price = string.IsNullOrEmpty(priceInput) ? existingProduct.Price : decimal.Parse(priceInput);

        Console.Write("Ange nytt lagerantal (lämna tomt för att behålla nuvarande): ");
        var stockInput = Console.ReadLine();
        var stock = string.IsNullOrEmpty(stockInput) ? existingProduct.Stock : int.Parse(stockInput);

        var updatedProduct = new Product
        {
            Id = id,
            Name = string.IsNullOrEmpty(name) ? existingProduct.Name : name,
            Category = string.IsNullOrEmpty(category) ? existingProduct.Category : category,
            Price = price,
            Stock = stock
        };

        _repository.Update(updatedProduct);
        Console.WriteLine($"Produkten med ID {id} har uppdaterats.");
    }

    private void DeleteProduct()
    {
        Console.Write("\nAnge ID för produkten du vill ta bort: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Ogiltigt ID!");
            return;
        }

        var product = _repository.GetById(id);
        if (product == null)
        {
            Console.WriteLine($"Ingen produkt hittades med ID {id}.");
            return;
        }

        _repository.Delete(id);
        Console.WriteLine($"Produkten med ID {id} har tagits bort.");
    }

    private void SearchByCategory()
    {
        Console.Write("\nAnge kategori-ID: ");
        if (!int.TryParse(Console.ReadLine(), out var categoryId))
        {
            Console.WriteLine("Ogiltigt kategori-ID!");
            return;
        }

        var products = _repository.GetProductsByCategory(categoryId);
        if (!products.Any())
        {
            Console.WriteLine($"Inga produkter hittades för kategori {categoryId}.");
            return;
        }

        Console.WriteLine($"Produkter i kategori {categoryId}:");
        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.Id}, Namn: {product.Name}, Pris: {product.Price}, Lager: {product.Stock}");
        }
    }
}
