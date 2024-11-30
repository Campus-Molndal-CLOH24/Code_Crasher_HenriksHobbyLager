using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Database;


namespace HenriksHobbyLager.Repository
{
    public class SqliteRepository : IRepository<Product>
    {
        private readonly SqliteDbcontext _sqliteDbcontext;
        public SqliteRepository(SqliteDbcontext sqliteDbcontext)
        {
            _sqliteDbcontext = sqliteDbcontext;
        }
        public IEnumerable<Product> GetAll()
        {
            return _sqliteDbcontext.Product.ToList();
        }

        public Product GetById(int id)
        {
            var product = _sqliteDbcontext.Product.Find(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} was not found");
            }
            return product;
        }

        public void Add(Product entity)
        {
            _sqliteDbcontext.Product.Add(entity);
            _sqliteDbcontext.SaveChanges();
        }   
        public void Update(Product entity)
        {
            _sqliteDbcontext.Product.Update(entity);
            _sqliteDbcontext.SaveChanges();
        }
        public void Delete(int id)
        {
            var product = _sqliteDbcontext.Product.Find(id);
            if (product != null)
                _sqliteDbcontext.Product.Remove(product);
            _sqliteDbcontext.SaveChanges();
        }
        public IEnumerable<Product> Search(Func<Product, bool> predicate)
        {
            return _sqliteDbcontext.Product.Where(predicate).ToList();
        }
    }
}
