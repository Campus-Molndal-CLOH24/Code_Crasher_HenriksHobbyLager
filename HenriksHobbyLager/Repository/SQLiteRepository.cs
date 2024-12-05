using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Database;
using System.Linq;

namespace HenriksHobbyLager.Repository
{
    public class SQLiteRepository(SqliteDbContext context) : IProductRepository<ProductSQLite>
    {
        private readonly SqliteDbContext _context = context;

        public IEnumerable<ProductSQLite> GetAll()
        {
            return _context.Products.ToList();
        }

        public ProductSQLite? GetById(string id)
        {
            int intId = int.Parse(id);
            return _context.Products.FirstOrDefault(p => p.Id == intId) ?? throw new KeyNotFoundException($"Produkt med ID {id} hittades inte.");
        }

        public void Create(ProductSQLite product)
        {
            if (_context.Products.Any(p => p.Id == product.Id))
            {
                throw new InvalidOperationException($"En produkt med ID {product.Id} finns redan i databasen.");
            }

            if (_context.Products.Any(p => p.Name == product.Name))
            {
                throw new InvalidOperationException($"En produkt med namnet '{product.Name}' finns redan i databasen.");
            }

            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Update(ProductSQLite product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            int intId = int.Parse(id);
            var product = _context.Products.FirstOrDefault(p => p.Id == intId);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public IEnumerable<ProductSQLite> Search(string searchText, bool predicate)
        {
            return _context.Products.Where(product =>
                product.Name.Contains(searchText) &&
                (predicate || product.Stock > 0)).ToList();
        }

        public IEnumerable<ProductSQLite> GetProductsByCategory(int categoryId)
        {
            return _context.Products.Where(p => p.CategoryId == categoryId).ToList();
        }
    }
}
