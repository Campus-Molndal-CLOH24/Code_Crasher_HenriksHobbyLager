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
        // // DbContext for interacting with the database
        private readonly DbContext _context;
        // DbSet for managing Product entities
        private readonly DbSet<Product> _dbSet;


        // Constructor for the Repository class
        // Constructor accepts the DbContext (injected via dependency injection)
        //Set for the Product entity
        public ProductRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<Product>();

        }

        // Get all entities from the database
        // Query products from the database using EF
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
        public async Task AddAsync(Product product)
        {
            var existingProduct = await _dbSet.FindAsync();
            if (existingProduct != null)
            {
                throw new InvalidOperationException($"Product with Id {product.Id} already exists.");
            }
            _dbSet.Add(product); //synconous version
            await _context.SaveChangesAsync();
        }
        // Update an entity in the database
        //use concurrency here too
        public async Task UpdateAsync(Product product)
        {
            try
            {
                _dbSet.Update(product);
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
            var product = await GetByIdAsync(id);
            _dbSet.Remove(product);
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
