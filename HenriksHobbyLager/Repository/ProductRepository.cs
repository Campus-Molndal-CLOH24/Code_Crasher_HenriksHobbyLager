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
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        // Get an entity by its ID
        public async Task<Product> GetByIdAsync(int id)
        {
            var property = typeof(Product).GetProperty("Id");
            if (property == null)
                throw new InvalidOperationException("T måste ha en Id-egenskap.");

            return await _dbSet.FindAsync(id) ??
                   throw new InvalidOperationException($"Entity with id {id} not found.");
        }

        // Add an entity to the database
        public async Task AddAsync(Product entity)
        {
            _dbSet.Add(entity); //synconous version
            await _context.SaveChangesAsync();
        }
        // Update an entity in the database
        //use concurrency here too
        public async Task UpdateAsync(Product entity)
        {
            try
            {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle the concurrency conflict
                var entry = ex.Entries.Single();
                var databaseValues = await entry.GetDatabaseValuesAsync();

                if (databaseValues == null)
                {
                    throw new InvalidOperationException("The product no longer exists in the database");
                }

                throw new InvalidOperationException("The product was modified by another user. Please refresh and try again.");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
        // Search for entities based on a predicate
        //need to use IQueryable<Product> becuase IEnumerable does not has async method
        //The key change is using a lambda expression x => predicate(x) instead of passing the predicate directly. 
        //This ensures the query remains as IQueryable which supports async operations.
        public async Task<IEnumerable<Product>> SearchAsync(Func<Product, bool> predicate)
        {
            return await _dbSet.Where(x => predicate(x)).ToListAsync();
        }
        //method for seaching by name
        //delected .AsEnumerable() to kepp IQuery running
        //i added .AsEnumerable() to make it work with code before
        public async Task<IEnumerable<Product>> SearchByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Enumerable.Empty<Product>();
            }
            return await _dbSet
                .Where(p => p.Name != null && p.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }
    }




}
