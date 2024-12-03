using HenriksHobbyLager.Database;
using HenriksHobbyLager.Facade;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Repository;
using HenriksHobbyLager.Service;

class Program
{
    static void Main()
    {
        // Fr�gar anv�ndaren vilken databas som ska anv�ndas, SQLite eller MongoDB.
        Console.WriteLine("V�lj databas: 1 f�r SQLite, 2 f�r MongoDB");
        var choice = Console.ReadLine();

        IProductRepository repository;

        // Om anv�ndaren v�ljer SQLite (val 1), skapa en SQLiteRepository.
        if (choice == "1")
        {
            var sqliteContext = new SqliteDbContext();
            repository = new SQLiteRepository(sqliteContext);
        }
        else
        {
            // Annars, skapa en MongoDBRepository.
            var mongoContext = new MongoDbContext();
            repository = new MongoDBRepository(mongoContext.Products); // Skickar IMongoCollection<Product> ist�llet f�r MongoDbContext
        }

        // Skapa en instans av ProductFacade med repository
        IProductFacade productFacade = new ProductFacade(repository);

        // Skapa en instans av ProductService med b�de productFacade och repository
        ProductService productService = new(productFacade, repository);

        // Skapa en instans av MenuService och starta menyn
        MenuService menuService = new(productService);
        menuService.DisplayMenu();  // Startar menyn
    }
}


