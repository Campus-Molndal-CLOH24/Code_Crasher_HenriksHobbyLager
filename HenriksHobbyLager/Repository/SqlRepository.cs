using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Database;
using Microsoft.EntityFrameworkCore;


namespace HenriksHobbyLager.Repository
{
    public class SqlRepository : IRepository<Product>
    {
        private readonly SqlDbcontext _context;
        public SqlRepository(SqlDbcontext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Product.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} was not found");
            }
            return product;
        }

        public async Task AddAsync(Product entity)
        {
            _context.Product.Add(entity);
            await _context.SaveChangesAsync();
        }   
        //try catch to handle concurrency issues
        //use permisstic concurrency to handle when multiple users are updating the same product
        // this concurrency is more detail about checked in the data what happen and tell user too
        //the simple one tell user just refresh and try again.
        //but this one tell user what happen and give more detail.
        public async Task UpdateAsync(Product entity)
        {
            try
            {
                _context.Product.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new InvalidOperationException("Someone else has modified this product. Please refresh and try again.", ex);
            }
        }
        public async Task DeleteAsync(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
                _context.Product.Remove(product);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Product>> SearchAsync(Func<Product, bool> predicate)
        {
            return await _context.Product
                .Where(x => predicate(x))
                .ToListAsync();
        }
    }
}
