using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Database;

namespace HenriksHobbyLager.Repository
{
    public class SqlRepository : IRepository<Product>
    {
        private readonly SqliteDbcontext _context;
        public SqlRepository(SqliteDbcontext context)
        {
            _context = context;
        }
        public IEnumerable<Product> GetAll()
        {
            return _context.Product.ToList();
        }

        public Product GetById(int id)
        {
            var product = _context.Product.Find(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} was not found");
            }
            return product;
        }

        public void Add(Product entity)
        {
            _context.Product.Add(entity);
            _context.SaveChanges();
        }   
        public void Update(Product entity)
        {
            _context.Product.Update(entity);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var product = _context.Product.Find(id);
            if (product != null)
                _context.Product.Remove(product);
            _context.SaveChanges();
        }
        public IEnumerable<Product> Search(Func<Product, bool> predicate)
        {
            return _context.Product.Where(predicate).ToList();
        }
    }
}
