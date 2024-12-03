# HenriksHobbyLager

HenriksHobbyLager �r ett hobbyprojekt som jag har byggt f�r att simulera lagerhantering av hobbyprodukter. Projektet �r implementerat i C# och anv�nder en kombination av SQLite och MongoDB f�r att hantera lagring av data. Systemet erbjuder ett menybaserat konsolgr�nssnitt f�r att visa, l�gga till, uppdatera, ta bort produkter, samt s�ka efter produkter utifr�n kategori.

## Installation
F�r att komma ig�ng med HenriksHobbyLager, f�lj dessa steg:

1. Klona det h�r repo:t till din lokala maskin.
   ```sh
   git clone <repo-url>
   ```
2. Installera n�dv�ndiga beroenden via NuGet:
   - `Microsoft.EntityFrameworkCore.Sqlite` f�r SQLite-databasen
3. �ppna projektet i Visual Studio och kontrollera att alla n�dv�ndiga paket �r installerade via NuGet Package Manager.

## Hur man k�r programmet
F�r att k�ra programmet f�lj dessa steg:

1. Bygg projektet i Visual Studio (F5).
2. G� till konsolgr�nssnittet och navigera genom menyn:
   - Du kommer f� m�jlighet att visa alla produkter, l�gga till nya produkter, uppdatera befintliga produkter, och s�ka efter produkter.
3. Anv�nd tangentbordet f�r att v�lja olika menyval, exempelvis att visa eller uppdatera produkter.

## Konfigurationsinst�llningar
- **SQLite Databas**: SQLite-databasen (�tkomlig via `HenriksHobbyLager.db`) anv�nds f�r lokal lagring av produktinformation. Databasanslutningen konfigureras i `SqliteDbContext` med s�kv�gen till den statiska databasfilen.

## Implementerade Patterns
- **Repository Pattern**: F�r att hantera �tkomst till databasen och CRUD-operationer anv�nds `ProductRepository` f�r SQLite. Dessa klasser fungerar som en abstraktion mellan databasen och den �vriga aff�rslogiken.
- **Facade Pattern**: `ProductFacade` anv�nds f�r att kapsla in komplexiteten med att anropa olika repository-klasser och f�renkla �tkomst till produktrelaterade operationer.

## Databasstruktur
- **SQLite (Produktlagring)**: En databas vid namn `HenriksHobbyLager.db` anv�nds f�r lokal lagring av produkter. Tabellen `Products` inneh�ller kolumnerna:
  - `Id`: Prim�rnyckel (INT)
  - `Name`: Produktnamn (TEXT)
  - `Price`: Pris p� produkten (REAL)
  - `Stock`: Antal i lager (INT)
  - `CategoryId`: ID f�r tillh�rande kategori (INT)

## Projektstruktur och Refaktorisering
Projektet �r organiserat enligt principen om Single Responsibility Principle (SRP), vilket inneb�r att varje klass och komponent har ett tydligt och avgr�nsat ansvar. Nedan f�ljer en beskrivning av hur koden �r uppdelad i olika filer och dess f�rdelar:

- **Database**: Denna mapp inneh�ller `SqliteDbContext` som ansvarar f�r att konfigurera och interagera med SQLite-databasen. Genom att ha en separat klass f�r databasanslutningen �r det enkelt att byta ut eller uppdatera databasinst�llningar utan att p�verka annan logik.

- **Facade**: Inneh�ller `ProductFacade`, som agerar som en enkel ing�ngspunkt till produktrelaterade operationer. Detta underl�ttar f�r �vriga klasser i applikationen genom att minska beroenden och g�ra det enkelt att �teranv�nda kod.

- **Interfaces**: Definierar de kontrakt som olika klasser implementerar, s�som `IProductRepository` och `IProductFacade`. Genom att anv�nda interfaces �r det l�tt att byta ut implementationer och skapa enhetstester som inte �r beroende av specifika klasser.

- **Migrations**: Inneh�ller migrationsfiler f�r att h�lla koll p� �ndringar i databasens struktur. Detta underl�ttar hanteringen av uppdateringar i databasens schema �ver tid.

- **Models**: Inneh�ller dom�nmodeller som `Product`. Dessa modeller representerar de dataobjekt som lagras i databasen och anv�nds i applikationen. Att h�lla modellerna separata g�r dem �teranv�ndbara och �kar kodens l�sbarhet.

- **Repository**: Inneh�ller klasser som `ProductRepository` och `SQLiteRepository` f�r databas�tkomst. Genom att hantera all databaslogik i repository-klasser separerar vi aff�rslogiken fr�n data�tkomsten, vilket f�rb�ttrar underh�llbarheten.

- **Service**: `ProductService` ansvarar f�r aff�rslogik relaterad till produkter. Detta �r ett exempel p� hur man kan samla verksamhetsregler och valideringar som inte b�r ligga i repository-klasserna.

- **UIs**: `MenuService` hanterar interaktionen med anv�ndaren via konsolmenyn. Genom att separera anv�ndargr�nssnittet fr�n �vriga delar av applikationen h�ller vi koden l�ttare att underh�lla och uppdatera.

- **Program.cs**: Detta �r huvudstartpunkten f�r programmet. H�r initialiseras alla komponenter och beroenden f�r att starta applikationen.

### F�rdelar med Projektstrukturen
1. **Enkel Underh�llbarhet**: Varje klass och komponent har ett tydligt ansvar. Detta f�renklar fels�kning och uppdateringar eftersom du vet var viss funktionalitet finns.
2. **B�ttre �teranv�ndbarhet**: Genom att bryta upp funktionaliteten i mindre, specialiserade klasser �r det enklare att �teranv�nda delar av koden utan att inf�ra o�nskade beroenden.
3. **F�rb�ttrad Testbarhet**: Genom att utnyttja interfaces och separera olika ansvarsomr�den �r det enklare att skapa enhetstester f�r varje del av applikationen utan att testa f�r mycket p� en g�ng.
4. **L�ttare Att Utvidga**: Med en tydlig struktur blir det enklare att l�gga till nya funktioner. Vill du exempelvis l�gga till en ny databas eller API kan du bara skapa ett nytt repository och konfigurera facaden att hantera det.

## Sammanfattning av Arbetet i Projektet

### 1. **Teknisk Implementation**
   - **SOLID-Principer**: Jag har implementerat flera SOLID-principer, framf�r allt **Single Responsibility Principle (SRP)** genom att dela upp ansvar mellan olika klasser som `ProductRepository`, `ProductService`, och `MenuService`. **Dependency Inversion Principle (DIP)** har ocks� implementerats genom att anv�nda interfaces som `IProductRepository` f�r att lossa beroenden mellan komponenter.
   
   - **Databasimplementation**: Projektet anv�nder SQLite f�r lokal datalagring, vilket m�jligg�r enkel hantering av produkter utan beroende av en molndatabas. Databasen hanteras genom `SqliteDbContext` som tillhandah�ller konfiguration och CRUD-operationer f�r produkterna.

   - **Anv�nda Patterns**: Jag har anv�nt **Repository Pattern** f�r att separera data�tkomstlogik fr�n aff�rslogik, vilket g�r koden mer underh�llbar och testbar. **Facade Pattern** anv�nds f�r att ge en f�renklad gr�nssnitt f�r produktrelaterade operationer, vilket underl�ttar f�r anv�ndaren och minskar komplexiteten i koden.

   - **Tekniska Utmaningar**: En av de st�rsta tekniska utmaningarna var hanteringen av `DuplicateKey`-fel i MongoDB. F�r att l�sa detta problem beh�vde jag se till att alla produkter fick unika `ObjectId`-v�rden vid ins�ttning, men jag valde slutligen att fokusera p� SQLite f�r att undvika ytterligare komplexitet.

### 2. **Arbetsprocess**
   - **Planering**: Projektet b�rjade med att definiera funktionerna f�r lagerhanteringen. Jag br�t ner funktionerna i mindre uppgifter och skapade en lista med tydliga m�l, s�som att implementera CRUD-operationer och skapa ett konsolgr�nssnitt.

   - **Val och Motivation**: Jag valde SQLite som databas eftersom det �r l�tt att s�tta upp och passar f�r lokala utvecklingsmilj�er. MongoDB testades ocks�, men efter problem med hantering av dubblettnycklar valde jag att g� vidare med enbart SQLite.

   - **L�rdomar**: Under projektets g�ng l�rde jag mig vikten av att ha tydligt avgr�nsade ansvarsomr�den i koden f�r att g�ra systemet underh�llbart. Att anv�nda repository- och facade-m�nster hj�lpte mig att strukturera systemet p� ett s�tt som �r l�tt att ut�ka och testa.

   - **Vad jag skulle g�ra annorlunda**: Om jag skulle g�ra om projektet skulle jag fr�n b�rjan fokusera mer p� att bygga automatiserade enhetstester f�r att s�kerst�lla kvalitet i varje steg av utvecklingen. Jag skulle �ven l�gga mer tid p� att konfigurera migrationshantering f�r databasen.

### 3. **Kod-exempel**
   - **Produktfacadens Anv�ndning**:
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
     **Varf�r det �r bra**: Detta kodexempel visar hur `Facade Pattern` anv�nds f�r att f�renkla �tkomsten till produktoperationer. Det minskar komplexiteten och kopplar bort beroenden.

   - **Repository Pattern**:
     ```csharp
     public void Create(Product product)
     {
         _context.Products.Add(product);
         _context.SaveChanges();
     }
     ```
     **Varf�r det �r bra**: Denna metod �r enkel och tydlig. Den isolerar databaslogiken och g�r det m�jligt att enkelt hantera �ndringar i hur data lagras.

   - **Anv�ndning av Interfaces**:
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
     **Varf�r det �r bra**: Att anv�nda `IProductRepository` h�r g�r koden flexibel. Det g�r det l�ttare att byta ut repository-implementationen utan att p�verka resten av applikationen.

## Sammanfattning
HenriksHobbyLager �r ett projekt som exemplifierar god kodstruktur och designm�nster f�r att hantera lagerhantering. Genom att anv�nda patterns som repository och facade, samt principer som SRP, har jag kunnat skapa ett system som �r l�tt att underh�lla, ut�ka och testa. Projektet har gett mig v�rdefull erfarenhet inom databasdesign, aff�rslogik och systemarkitektur.
