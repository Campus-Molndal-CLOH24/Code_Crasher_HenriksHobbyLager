using HenriksHobbyLager.Database;
using HenriksHobbyLager.Facade;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Repository;
using HenriksHobbyLager.Service;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Välj databas: 1 för SQLite, 2 för MongoDB");
        var choice = Console.ReadLine();

        IProductRepository repository;

        if (choice == "1")
        {
            var sqliteContext = new SqliteDbContext();
            repository = new SQLiteRepository(sqliteContext);
        }
        else
        {
            var mongoContext = new MongoDbContext();
            repository = new MongoDBRepository(mongoContext);
        }

        var productFacade = new ProductFacade(repository);
        var productService = new ProductService(productFacade);

        // Starta menyn eller sök efter produkter efter kategori
        productService.SearchByCategory(1); // Exempelanrop, du kan byta till din menyhantering
    }
}
