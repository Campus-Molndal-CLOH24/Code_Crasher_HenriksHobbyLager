class Program
{
    static void Main()
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

        // Skapa en instans av ProductFacade med repository
        IProductFacade productFacade = new ProductFacade(repository);

        // Skapa en instans av ProductService med både productFacade och repository
        ProductService productService = new ProductService(productFacade, repository);

        // Skapa en instans av MenuService och starta menyn
        MenuService menuService = new MenuService(productService);
        menuService.DisplayMenu();  // Startar menyn
    }
}
