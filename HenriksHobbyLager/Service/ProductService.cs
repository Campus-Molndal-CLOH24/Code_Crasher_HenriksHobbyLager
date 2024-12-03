using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Facade;
using HenriksHobbyLager.Database;
using HenriksHobbyLager.Repository;

namespace HenriksHobbyLager.Service
{
    // Denna klass hanterar logiken för produktrelaterade operationer, som CRUD-operationer och visning av produkter.
    public class ProductService
    {
        private readonly IProductFacade _productFacade;
        private readonly IProductRepository _repository;

        // Konstruktor som initialiserar produktfacaden och repository, och kastar undantag om någon av dem är null.
        public ProductService(IProductFacade productFacade, IProductRepository repository)
        {
            _productFacade = productFacade ?? throw new ArgumentNullException(nameof(productFacade));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        // Metod för att visa alla produkter.
        public void DisplayAllProducts()
        {
            Console.WriteLine("\nAlla produkter:");
            var products = _repository.GetAll();

            // Om inga produkter hittas, skrivs ett meddelande ut och metoden avslutas.
            if (!products.Any())
            {
                Console.WriteLine("Inga produkter hittades.");
                return;
            }

            // Itererar genom alla produkter och skriver ut deras egenskaper.
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Namn: {product.Name}, Pris: {product.Price}, Lager: {product.Stock}");
            }
        }

        // Metod för att lägga till en ny produkt.
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

            // Skapar ett nytt produktobjekt baserat på användarens inmatning.
            var product = new Product
            {
                Name = name ?? "Okänd",
                CategoryId = categoryId,
                Price = price,
                Stock = stock
            };

            // Lägger till produkten i repository.
            _repository.Create(product);
            Console.WriteLine($"Produkten '{name}' har lagts till.");
        }

        // Metod för att uppdatera en produkt.
        public void UpdateProduct()
        {
            Console.Write("\nAnge ID för produkten du vill uppdatera: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("Ogiltigt ID!");
                return;
            }

            // Hämtar den befintliga produkten baserat på ID.
            var existingProduct = _repository.GetById(id);
            if (existingProduct == null)
            {
                Console.WriteLine($"Ingen produkt hittades med ID {id}.");
                return;
            }

            // Frågar användaren om nya värden för produktens egenskaper.
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

            // Skapar ett uppdaterat produktobjekt baserat på användarens inmatning.
            var updatedProduct = new Product
            {
                Id = id,
                Name = string.IsNullOrEmpty(name) ? existingProduct.Name : name,
                CategoryId = string.IsNullOrEmpty(category) ? existingProduct.CategoryId : int.Parse(category),
                Price = price,
                Stock = stock
            };

            // Uppdaterar produkten i repository.
            _repository.Update(updatedProduct);
            Console.WriteLine($"Produkten med ID {id} har uppdaterats.");
        }

        // Metod för att ta bort en produkt.
        public void DeleteProduct()
        {
            Console.Write("\nAnge ID för produkten du vill ta bort: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("Ogiltigt ID!");
                return;
            }

            // Hämtar produkten baserat på ID och tar bort den om den finns.
            var product = _repository.GetById(id);
            if (product == null)
            {
                Console.WriteLine($"Ingen produkt hittades med ID {id}.");
                return;
            }

            _repository.Delete(id);
            Console.WriteLine($"Produkten med ID {id} har tagits bort.");
        }

        // Metod för att söka efter produkter baserat på kategori-ID.
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

                // Skriver ut alla produkter som tillhör den specifika kategorin.
                Console.WriteLine("Produkter i vald kategori:");
                foreach (var product in products)
                {
                    Console.WriteLine($"ID: {product.Id}, Namn: {product.Name}, Pris: {product.Price:C}, Lager: {product.Stock}");
                }
            }
            catch (Exception ex)
            {
                // Hanterar eventuella undantag som kan uppstå.
                Console.WriteLine($"Ett fel uppstod: {ex.Message}");
            }
        }
    }
}


