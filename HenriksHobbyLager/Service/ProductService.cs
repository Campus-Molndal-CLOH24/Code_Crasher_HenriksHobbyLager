using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Facade;
using HenriksHobbyLager.Database;
using HenriksHobbyLager.Repository;

namespace HenriksHobbyLager.Service
{
    public class ProductService(IProductFacade productFacade)
    {
        private readonly IProductRepository _repository;
        private readonly object _productFacade = productFacade ?? throw new ArgumentNullException(nameof(productFacade));
        private readonly ProductFacade productFacade;
        private readonly IProductFacade _productfacade;
      

        public void DisplayAllProducts()
        {
            Console.WriteLine("\nAlla produkter:");
            var products = _repository.GetAll();

            if (!products.Any())
            {
                Console.WriteLine("Inga produkter hittades.");
                return;
            }

            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Namn: {product.Name}, Pris: {product.Price}, Lager: {product.Stock}");
            }
        }

        public void AddProduct()
        {
            Console.Write("\nAnge produktnamn: ");
            var name = Console.ReadLine();

            Console.Write("Ange kategori-ID: ");
            if (!int.TryParse(Console.ReadLine(), out var categoryId))
            {
                Console.WriteLine("Ogiltigt kategori-ID!");
                return;
            }

            Console.Write("Ange pris: ");
            if (!decimal.TryParse(Console.ReadLine(), out var price))
            {
                Console.WriteLine("Ogiltigt pris!");
                return;
            }

            Console.Write("Ange lagerantal: ");
            if (!int.TryParse(Console.ReadLine(), out var stock))
            {
                Console.WriteLine("Ogiltigt lagerantal!");
                return;
            }

            var product = new Product
            {
                Name = name ?? "Okänd",
                CategoryId = categoryId,
                Price = price,
                Stock = stock
            };

            _repository.Create(product);
            Console.WriteLine($"Produkten '{name}' har lagts till.");
        }

        public void UpdateProduct()
        {
            Console.Write("\nAnge ID för produkten du vill uppdatera: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("Ogiltigt ID!");
                return;
            }

            var existingProduct = _repository.GetById(id);
            if (existingProduct == null)
            {
                Console.WriteLine($"Ingen produkt hittades med ID {id}.");
                return;
            }

            Console.Write("Ange nytt namn (lämna tomt för att behålla nuvarande): ");
            var name = Console.ReadLine();
            Console.Write("Ange ny kategori (lämna tomt för att behålla nuvarande): ");
            var category = Console.ReadLine();

            Console.Write("Ange nytt pris (lämna tomt för att behålla nuvarande): ");
            var priceInput = Console.ReadLine();
            var price = string.IsNullOrEmpty(priceInput) ? existingProduct.Price : decimal.Parse(priceInput);

            Console.Write("Ange nytt lagerantal (lämna tomt för att behålla nuvarande): ");
            var stockInput = Console.ReadLine();
            var stock = string.IsNullOrEmpty(stockInput) ? existingProduct.Stock : int.Parse(stockInput);

            var updatedProduct = new Product
            {
                Id = id,
                Name = string.IsNullOrEmpty(name) ? existingProduct.Name : name,
                CategoryId = string.IsNullOrEmpty(category) ? existingProduct.CategoryId : int.Parse(category),
                Price = price,
                Stock = stock
            };

            _repository.Update(updatedProduct);
            Console.WriteLine($"Produkten med ID {id} har uppdaterats.");
        }

        public void DeleteProduct()
        {
            Console.Write("\nAnge ID för produkten du vill ta bort: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("Ogiltigt ID!");
                return;
            }

            var product = _repository.GetById(id);
            if (product == null)
            {
                Console.WriteLine($"Ingen produkt hittades med ID {id}.");
                return;
            }

            _repository.Delete(id);
            Console.WriteLine($"Produkten med ID {id} har tagits bort.");
        }

        public void SearchByCategory(int categoryId)
        {
            try
            {
                var products = _repository.GetProductsByCategory(categoryId);
                if (products == null || !products.Any())
                {
                    Console.WriteLine("Inga produkter hittades i denna kategori.");
                    return;
                }

                Console.WriteLine("Produkter i vald kategori:");
                foreach (var product in products)
                {
                    Console.WriteLine($"ID: {product.Id}, Namn: {product.Name}, Pris: {product.Price:C}, Lager: {product.Stock}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel uppstod: {ex.Message}");
            }
        }


        internal void SearchByCategory()
        {
            throw new NotImplementedException();
        }
    }
}

