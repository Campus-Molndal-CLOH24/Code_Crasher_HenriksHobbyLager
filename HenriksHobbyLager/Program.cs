using HenriksHobbyLager.Database;
using HenriksHobbyLager.Facade;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Repository;
using HenriksHobbyLager.Service;

class Program
{
    static void Main()
    {
        Console.WriteLine("V�lj databas: 1 f�r SQLite, 2 f�r MongoDB");
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
            repository = new MongoDBRepository(mongoContext.Products); // Pass the IMongoCollection<Product> instead of MongoDbContext
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
