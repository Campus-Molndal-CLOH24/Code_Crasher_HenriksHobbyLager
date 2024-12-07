using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Database;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace HenriksHobbyLager.Repository
{
    public class SqliteRepository : IRepository<Product>
    {
        private readonly SqliteDbcontext _sqliteDbcontext;
        public SqliteRepository(SqliteDbcontext sqliteDbcontext)
        {
            _sqliteDbcontext = sqliteDbcontext;
        }
        

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _sqliteDbcontext.Product.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _sqliteDbcontext.Product.FindAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} was not found");
            }
            return product;
        }

        public async Task AddAsync(Product entity)
        {
            _sqliteDbcontext.Product.Add(entity);
            await _sqliteDbcontext.SaveChangesAsync();
        }
        //try catch to handle concurrency issues
        //use permisstic concurrency to handle when multiple users are updating the same product
        public async Task UpdateAsync(Product entity)
        {
            try
            {
                _sqliteDbcontext.Product.Update(entity);
                await _sqliteDbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new InvalidOperationException("Someone else has modified this product. Please refresh and try again.", ex);
            }
        }
        public async Task DeleteAsync(int id)
        {
            var product = await _sqliteDbcontext.Product.FindAsync(id);
            if (product != null)
                _sqliteDbcontext.Product.Remove(product);
            await _sqliteDbcontext.SaveChangesAsync();
        }
        public async Task<IEnumerable<Product>> SearchAsync(Func<Product, bool> predicate)
        {
            return await _sqliteDbcontext.Product
                .Include(p => p.Category) // using eager loading to load the related Category data
                .Where(x => predicate(x))
                .ToListAsync();
        }
    }
}
