using System;
using Microsoft.EntityFrameworkCore;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Database;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Repository;
using HenriksHobbyLager.Facade;
using HenriksHobbyLager.Services;
using MongoDB.Driver;


namespace HenriksHobbyLager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("V�lj databas: 1 f�r SQLite, 2 f�r MongoDB");
            var choice = Console.ReadLine();

            IProductRepository repository;

            if (choice == "1")
            {
                var sqliteContext = new SqliteDbContext();
                repository = new SQLiteRepository(sqliteContext);
            }
            else
            {
                var mongoContext = new MongoDbContext();
                repository = new MongoDBRepository(mongoContext);
            }

            // Skapa en instans av ProductFacade och injicera repository
            IProductFacade productFacade = new ProductFacade(repository);

            // Skapa ProductService och injicera ProductFacade
            var productService = new ProductService(productFacade);

            // Starta tj�nsten - till exempel en menyfunktion
            productService.DisplayAllProducts(); // Detta kan du byta ut mot en meny eller annan metod
        }
    }
}
