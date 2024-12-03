namespace HenriksHobbyLager.Service
{
    public class MenuService
    {
        private readonly ProductService _productService;

        public MenuService(ProductService productService)
        {
            _productService = productService;
        }

        public void DisplayMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("******** Henriks Hobby Lager ********");
                Console.WriteLine("1. Visa alla produkter");
                Console.WriteLine("2. Lägg till en produkt");
                Console.WriteLine("3. Uppdatera en produkt");
                Console.WriteLine("4. Ta bort en produkt");
                Console.WriteLine("5. Hämta produkter per kategori");
                Console.WriteLine("6. Avsluta");
                Console.WriteLine("****************************************");
                Console.Write("Välj ett alternativ: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        _productService.DisplayAllProducts();
                        break;
                    case "2":
                        _productService.AddProduct();
                        break;
                    case "3":
                        _productService.UpdateProduct();
                        break;
                    case "4":
                        _productService.DeleteProduct();
                        break;
                    case "5":
                        Console.Write("\nAnge kategori-ID för att söka efter produkter: ");
                        if (int.TryParse(Console.ReadLine(), out int categoryId))
                        {
                            _productService.SearchByCategory(categoryId);
                        }
                        else
                        {
                            Console.WriteLine("Ogiltigt kategori-ID!");
                        }
                        break;
                    case "6":
                        Console.WriteLine("Avslutar programmet...");
                        return;
                    default:
                        Console.WriteLine("Ogiltigt val! Försök igen.");
                        break;
                }

                Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
            }
        }
    }
}
