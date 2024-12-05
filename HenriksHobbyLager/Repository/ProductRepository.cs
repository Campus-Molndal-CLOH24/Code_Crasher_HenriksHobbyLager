using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;

namespace HenriksHobbyLager.Repository
{
    public class ProductRepository<T>(IProductRepository<T> repository) : IProductRepository<T>
    {
        private readonly IProductRepository<T> _repository = repository;

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public T? GetById(string id)
        {
            return _repository.GetById(id);
        }

        public void Create(T product)
        {
            _repository.Create(product);
        }

        public void Update(T product)
        {
            _repository.Update(product);
        }

        public void Delete(string id)
        {
            _repository.Delete(id);
        }

        public IEnumerable<T> Search(string searchText, bool predicate)
        {
            return _repository.Search(searchText, predicate);
        }

        public IEnumerable<T> GetProductsByCategory(int categoryId)
        {
            if (typeof(T) == typeof(ProductSQLite))
            {
                var sqliteRepo = _repository as IProductRepository<ProductSQLite>;
                var sqliteProducts = sqliteRepo?.GetProductsByCategory(categoryId) ?? Enumerable.Empty<ProductSQLite>();
                return sqliteProducts.Cast<T>(); // Typkonvertering till IEnumerable<T>
            }
            else if (typeof(T) == typeof(ProductMongo))
            {
                var mongoRepo = _repository as IProductRepository<ProductMongo>;
                var mongoProducts = mongoRepo?.GetProductsByCategory(categoryId) ?? Enumerable.Empty<ProductMongo>();
                return mongoProducts.Cast<T>(); // Typkonvertering till IEnumerable<T>
            }
            else
            {
                throw new NotSupportedException("GetProductsByCategory stöds inte för denna typ.");
            }
        }
    }
}

