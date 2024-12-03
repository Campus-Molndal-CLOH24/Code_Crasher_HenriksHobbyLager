# HenriksHobbyLager

HenriksHobbyLager är ett hobbyprojekt som jag har byggt för att simulera lagerhantering av hobbyprodukter. Projektet är implementerat i C# och använder en kombination av SQLite och MongoDB för att hantera lagring av data. Systemet erbjuder ett menybaserat konsolgränssnitt för att visa, lägga till, uppdatera, ta bort produkter, samt söka efter produkter utifrån kategori.

## Installation
För att komma igång med HenriksHobbyLager, följ dessa steg:

1. Klona det här repo:t till din lokala maskin.
   ```sh
   git clone <repo-url>
   ```
2. Installera nödvändiga beroenden via NuGet:
   - `Microsoft.EntityFrameworkCore.Sqlite` för SQLite-databasen
3. Öppna projektet i Visual Studio och kontrollera att alla nödvändiga paket är installerade via NuGet Package Manager.

## Hur man kör programmet
För att köra programmet följ dessa steg:

1. Bygg projektet i Visual Studio (F5).
2. Gå till konsolgränssnittet och navigera genom menyn:
   - Du kommer få möjlighet att visa alla produkter, lägga till nya produkter, uppdatera befintliga produkter, och söka efter produkter.
3. Använd tangentbordet för att välja olika menyval, exempelvis att visa eller uppdatera produkter.

## Konfigurationsinställningar
- **SQLite Databas**: SQLite-databasen (åtkomlig via `HenriksHobbyLager.db`) används för lokal lagring av produktinformation. Databasanslutningen konfigureras i `SqliteDbContext` med sökvägen till den statiska databasfilen.

## Implementerade Patterns
- **Repository Pattern**: För att hantera åtkomst till databasen och CRUD-operationer används `ProductRepository` för SQLite. Dessa klasser fungerar som en abstraktion mellan databasen och den övriga affärslogiken.
- **Facade Pattern**: `ProductFacade` används för att kapsla in komplexiteten med att anropa olika repository-klasser och förenkla åtkomst till produktrelaterade operationer.

## Databasstruktur
- **SQLite (Produktlagring)**: En databas vid namn `HenriksHobbyLager.db` används för lokal lagring av produkter. Tabellen `Products` innehåller kolumnerna:
  - `Id`: Primärnyckel (INT)
  - `Name`: Produktnamn (TEXT)
  - `Price`: Pris på produkten (REAL)
  - `Stock`: Antal i lager (INT)
  - `CategoryId`: ID för tillhörande kategori (INT)

## Projektstruktur och Refaktorisering
Projektet är organiserat enligt principen om Single Responsibility Principle (SRP), vilket innebär att varje klass och komponent har ett tydligt och avgränsat ansvar. Nedan följer en beskrivning av hur koden är uppdelad i olika filer och dess fördelar:

- **Database**: Denna mapp innehåller `SqliteDbContext` som ansvarar för att konfigurera och interagera med SQLite-databasen. Genom att ha en separat klass för databasanslutningen är det enkelt att byta ut eller uppdatera databasinställningar utan att påverka annan logik.

- **Facade**: Innehåller `ProductFacade`, som agerar som en enkel ingångspunkt till produktrelaterade operationer. Detta underlättar för övriga klasser i applikationen genom att minska beroenden och göra det enkelt att återanvända kod.

- **Interfaces**: Definierar de kontrakt som olika klasser implementerar, såsom `IProductRepository` och `IProductFacade`. Genom att använda interfaces är det lätt att byta ut implementationer och skapa enhetstester som inte är beroende av specifika klasser.

- **Migrations**: Innehåller migrationsfiler för att hålla koll på ändringar i databasens struktur. Detta underlättar hanteringen av uppdateringar i databasens schema över tid.

- **Models**: Innehåller domänmodeller som `Product`. Dessa modeller representerar de dataobjekt som lagras i databasen och används i applikationen. Att hålla modellerna separata gör dem återanvändbara och ökar kodens läsbarhet.

- **Repository**: Innehåller klasser som `ProductRepository` och `SQLiteRepository` för databasåtkomst. Genom att hantera all databaslogik i repository-klasser separerar vi affärslogiken från dataåtkomsten, vilket förbättrar underhållbarheten.

- **Service**: `ProductService` ansvarar för affärslogik relaterad till produkter. Detta är ett exempel på hur man kan samla verksamhetsregler och valideringar som inte bör ligga i repository-klasserna.

- **UIs**: `MenuService` hanterar interaktionen med användaren via konsolmenyn. Genom att separera användargränssnittet från övriga delar av applikationen håller vi koden lättare att underhålla och uppdatera.

- **Program.cs**: Detta är huvudstartpunkten för programmet. Här initialiseras alla komponenter och beroenden för att starta applikationen.

### Fördelar med Projektstrukturen
1. **Enkel Underhållbarhet**: Varje klass och komponent har ett tydligt ansvar. Detta förenklar felsökning och uppdateringar eftersom du vet var viss funktionalitet finns.
2. **Bättre Återanvändbarhet**: Genom att bryta upp funktionaliteten i mindre, specialiserade klasser är det enklare att återanvända delar av koden utan att införa oönskade beroenden.
3. **Förbättrad Testbarhet**: Genom att utnyttja interfaces och separera olika ansvarsområden är det enklare att skapa enhetstester för varje del av applikationen utan att testa för mycket på en gång.
4. **Lättare Att Utvidga**: Med en tydlig struktur blir det enklare att lägga till nya funktioner. Vill du exempelvis lägga till en ny databas eller API kan du bara skapa ett nytt repository och konfigurera facaden att hantera det.

## Sammanfattning av Arbetet i Projektet

### 1. **Teknisk Implementation**
   - **SOLID-Principer**: Jag har implementerat flera SOLID-principer, framför allt **Single Responsibility Principle (SRP)** genom att dela upp ansvar mellan olika klasser som `ProductRepository`, `ProductService`, och `MenuService`. **Dependency Inversion Principle (DIP)** har också implementerats genom att använda interfaces som `IProductRepository` för att lossa beroenden mellan komponenter.
   
   - **Databasimplementation**: Projektet använder SQLite för lokal datalagring, vilket möjliggör enkel hantering av produkter utan beroende av en molndatabas. Databasen hanteras genom `SqliteDbContext` som tillhandahåller konfiguration och CRUD-operationer för produkterna.

   - **Använda Patterns**: Jag har använt **Repository Pattern** för att separera dataåtkomstlogik från affärslogik, vilket gör koden mer underhållbar och testbar. **Facade Pattern** används för att ge en förenklad gränssnitt för produktrelaterade operationer, vilket underlättar för användaren och minskar komplexiteten i koden.

   - **Tekniska Utmaningar**: En av de största tekniska utmaningarna var hanteringen av `DuplicateKey`-fel i MongoDB. För att lösa detta problem behövde jag se till att alla produkter fick unika `ObjectId`-värden vid insättning, men jag valde slutligen att fokusera på SQLite för att undvika ytterligare komplexitet.

### 2. **Arbetsprocess**
   - **Planering**: Projektet började med att definiera funktionerna för lagerhanteringen. Jag bröt ner funktionerna i mindre uppgifter och skapade en lista med tydliga mål, såsom att implementera CRUD-operationer och skapa ett konsolgränssnitt.

   - **Val och Motivation**: Jag valde SQLite som databas eftersom det är lätt att sätta upp och passar för lokala utvecklingsmiljöer. MongoDB testades också, men efter problem med hantering av dubblettnycklar valde jag att gå vidare med enbart SQLite.

   - **Lärdomar**: Under projektets gång lärde jag mig vikten av att ha tydligt avgränsade ansvarsområden i koden för att göra systemet underhållbart. Att använda repository- och facade-mönster hjälpte mig att strukturera systemet på ett sätt som är lätt att utöka och testa.

   - **Vad jag skulle göra annorlunda**: Om jag skulle göra om projektet skulle jag från början fokusera mer på att bygga automatiserade enhetstester för att säkerställa kvalitet i varje steg av utvecklingen. Jag skulle även lägga mer tid på att konfigurera migrationshantering för databasen.

### 3. **Kod-exempel**
   - **Produktfacadens Användning**:
     ```csharp
     public class ProductFacade
     {
         private readonly IProductRepository _repository;

         public ProductFacade(IProductRepository repository)
         {
             _repository = repository;
         }

         public IEnumerable<Product> GetAllProducts()
         {
             return _repository.GetAll();
         }
     }
     ```
     **Varför det är bra**: Detta kodexempel visar hur `Facade Pattern` används för att förenkla åtkomsten till produktoperationer. Det minskar komplexiteten och kopplar bort beroenden.

   - **Repository Pattern**:
     ```csharp
     public void Create(Product product)
     {
         _context.Products.Add(product);
         _context.SaveChanges();
     }
     ```
     **Varför det är bra**: Denna metod är enkel och tydlig. Den isolerar databaslogiken och gör det möjligt att enkelt hantera ändringar i hur data lagras.

   - **Användning av Interfaces**:
     ```csharp
     public class ProductService
     {
         private readonly IProductRepository _repository;

         public ProductService(IProductRepository repository)
         {
             _repository = repository;
         }

         public void AddProduct(Product product)
         {
             _repository.Create(product);
         }
     }
     ```
     **Varför det är bra**: Att använda `IProductRepository` här gör koden flexibel. Det gör det lättare att byta ut repository-implementationen utan att påverka resten av applikationen.

## Sammanfattning
HenriksHobbyLager är ett projekt som exemplifierar god kodstruktur och designmönster för att hantera lagerhantering. Genom att använda patterns som repository och facade, samt principer som SRP, har jag kunnat skapa ett system som är lätt att underhålla, utöka och testa. Projektet har gett mig värdefull erfarenhet inom databasdesign, affärslogik och systemarkitektur.
