using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Database;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Repository;
using Microsoft.EntityFrameworkCore;

namespace Ui;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Välj databas: 1 för SQLite, 2 för MongoDB");
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

        var productService = new ProductService(repository);

        // Starta menyn
        productService.DisplayMenu();
    }
}
