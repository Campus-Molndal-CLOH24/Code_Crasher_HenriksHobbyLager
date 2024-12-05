using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace HenriksHobbyLager.Repository
{
    public class MongoDBRepository(IMongoDatabase database) : IProductRepository<ProductMongo>
    {
        private readonly IMongoCollection<ProductMongo> _products = database.GetCollection<ProductMongo>("Products");

        public IEnumerable<ProductMongo> GetAll()
        {
            return _products.Find(product => true).ToList();
        }

        public ProductMongo? GetById(string id)
        {
            return _products.Find(product => product.Id == id).FirstOrDefault();
        }

        public void Create(ProductMongo product)
        {
            try
            {
                if (_products.Find(p => p.Id == product.Id).Any())
                {
                    throw new InvalidOperationException($"En produkt med ID {product.Id} finns redan i databasen.");
                }

                _products.InsertOne(product);
            }
            catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
            {
                Console.WriteLine("Fel: Produkten kunde inte läggas till eftersom ID redan existerar.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett oväntat fel uppstod: {ex.Message}");
            }
        }

        public void Update(ProductMongo product)
        {
            _products.ReplaceOne(p => p.Id == product.Id, product);
        }

        public void Delete(string id)
        {
            _products.DeleteOne(product => product.Id == id);
        }

        public IEnumerable<ProductMongo> Search(string searchText, bool predicate)
        {
            return _products.Find(product =>
                product.Name.Contains(searchText) &&
                (predicate || product.Stock > 0)).ToList();
        }

        public IEnumerable<ProductMongo> GetProductsByCategory(int categoryId)
        {
            return _products.Find(product => product.CategoryId == categoryId).ToList();
        }
    }
}
