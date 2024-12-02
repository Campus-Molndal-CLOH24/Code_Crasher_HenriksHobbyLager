using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Repository;
using Microsoft.EntityFrameworkCore;

namespace HenriksHobbyLager.Database;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext()
    {
        var client = new MongoClient("mongodb://localhost:27017"); // Anpassa anslutningssträngen
        _database = client.GetDatabase("HenriksHobbyLager");
    }

    public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");
}

