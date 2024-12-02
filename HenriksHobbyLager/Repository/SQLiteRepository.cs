﻿using System.Collections.Generic;
using System.Linq;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Database;
using Microsoft.EntityFrameworkCore;

namespace HenriksHobbyLager.Repository;


public class SQLiteRepository : IProductRepository
{
    private readonly SqliteDbContext _context;

    public SQLiteRepository(SqliteDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Product> GetAll()
    {
        return _context.Products.ToList();
    }

    public Product? GetById(int id)
    {
        return _context.Products.Find(id);
    }

    public void Add(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public void Update(Product product)
    {
        _context.Products.Update(product);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var product = GetById(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }

    public IEnumerable<Product> GetProductsByCategory(int categoryId)
    {
        return _context.Products.Where(p => p.CategoryId == categoryId).ToList();
    }

    public IEnumerable<Product> Search(Func<Product, bool> predicate)
    {
        return _context.Products.Where(predicate).ToList();
    }
}
