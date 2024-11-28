using System;
using System.Collections.Generic;
using System.Linq;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Database;
using Microsoft.EntityFrameworkCore;


namespace HenriksHobbyLager.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        // Database context for managing database connections and operations
        private readonly DbContext _context;
        // DbSet for managing the entities of type T
        private readonly DbSet<T> _dbSet;

        
        // Constructor for the Repository class
        // Constructor accepts the DbContext (injected via dependency injection)
        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
            
        }

        // Get all entities from the database
        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }
        // Get an entity by its ID
        public T GetById(int id)
        {
            var property = typeof(T).GetProperty("Id");
            if (property == null)
                throw new InvalidOperationException("T måste ha en Id-egenskap.");

            return _dbSet.Find(id) ?? 
                   throw new InvalidOperationException($"Entity with id {id} not found.");
        }

        // Add an entity to the database
        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }
        // Update an entity in the database
        public void Update(T entity)
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
        public IEnumerable<T> Search(Func<T, bool> predicate)
        {
            return _dbSet.Where(predicate);
        }

        
    }
}
