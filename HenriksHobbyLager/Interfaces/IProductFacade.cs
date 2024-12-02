using System;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Database;
using HenriksHobbyLager.Repository;
using HenriksHobbyLager;

    namespace HenriksHobbyLager.Interfaces
    {
        public interface IProductFacade
        {
            IEnumerable<Product> GetAllProducts();
            Product GetProductById(int id);
            void CreateProduct(Product product);
            void UpdateProduct(Product product);
            void DeleteProduct(int id);
            IEnumerable<Product> SearchProducts(Func<Product, bool> predicate);
        }
    }
