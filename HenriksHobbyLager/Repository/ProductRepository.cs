using System;
using System.Linq;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Database;
using Microsoft.EntityFrameworkCore;
using HenriksHobbyLager.Models;



namespace HenriksHobbyLager.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        // Database context for managing database connections and operations
        private readonly DbContext _context;
        // DbSet for managing the entities of type T
        private readonly DbSet<Product> _dbSet;


        // Constructor for the Repository class
        // Constructor accepts the DbContext (injected via dependency injection)
        public ProductRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<Product>();

        }

        // Get all entities from the database
        public IEnumerable<Product> GetAll()
        {
            return _dbSet.ToList();
        }
        // Get an entity by its ID
        public Product GetById(int id)
        {
            var property = typeof(Product).GetProperty("Id");
            if (property == null)
                throw new InvalidOperationException("T måste ha en Id-egenskap.");

            return _dbSet.Find(id) ??
                   throw new InvalidOperationException($"Entity with id {id} not found.");
        }

        // Add an entity to the database
        public void Add(Product entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }
        // Update an entity in the database
        public void Update(Product entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
        // Search for entities based on a predicate
        public IEnumerable<Product> Search(Func<Product, bool> predicate)
        {
            return _dbSet.Where(predicate);
        }
        //method for seaching by name
        public IEnumerable<Product> SearchByName(string name)
        {
            if(string.IsNullOrEmpty(name)){
                return Enumerable.Empty<Product>();
            }
            return _dbSet
                .AsEnumerable()
                .Where(p => p.Name != null && p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }
    }
    

    

}
