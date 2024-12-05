```markdown
# HenriksHobbyLager

HenriksHobbyLager är ett hobbyprojekt som simulerar lagerhantering av hobbyprodukter. Projektet är byggt i C# och använder SQLite och MongoDB för datahantering. Syftet är att demonstrera en tydlig arkitektur, implementering av designmönster och separering av ansvar i koden. Applikationen erbjuder ett menybaserat konsolgränssnitt för att visa, lägga till, uppdatera, ta bort samt söka produkter.

## Installation

1. Klona detta repository till din lokala maskin:  
   ```sh
   git clone <repo-url>
   ```

2. Installera nödvändiga beroenden via NuGet, till exempel:  
   - `Microsoft.EntityFrameworkCore.Sqlite` för SQLite-hantering.

3. Öppna projektet i Visual Studio och se till att alla paket är installerade.

## Hur man kör programmet

1. Bygg projektet i Visual Studio (F5).
2. Navigera genom konsolmenyn för att visa produkter, lägga till nya, uppdatera befintliga eller söka efter produkter.
3. Använd tangentbordet för att göra menyval.

## Konfigurationsinställningar

- **SQLite-databas**: En lokal SQLite-databas används för att lagra produktinformation. `SqliteDbContext` konfigurerar och interagerar med databasfilen `HenriksHobbyLager.db`.

## Implementerade Patterns

- **Repository Pattern**: Används för att separera dataåtkomstlogik från affärslogik. Detta gör koden mer lättunderhållen och testbar.
- **Facade Pattern**: `ProductFacade` erbjuder en enkel ingångspunkt till produktrelaterade operationer, vilket minskar komplexiteten för resten av applikationen.

## Databasstruktur

- **SQLite**:  
  Tabellen `Products` innehåller:  
  - `Id` (Primärnyckel, INT)  
  - `Name` (Produktnamn, TEXT)  
  - `Price` (Pris, REAL)  
  - `Stock` (Antal i lager, INT)  
  - `CategoryId` (ID för kategori, INT)

## Projektstruktur och Refaktorisering

Koden är organiserad för att följa Single Responsibility Principle, vilket ökar läsbarhet och underhållbarhet. Den uppdaterade filstrukturen visas nedan:

```bash
HenriksHobbyLager/
├─ Database/
│  ├─ SqliteDbContext.cs
│  └─ MongoDbContext.cs
├─ Facade/
│  └─ ProductFacade.cs
├─ Interfaces/
│  ├─ IProductRepository.cs
│  ├─ IProductFacade.cs
│  └─ IProductBase.cs
├─ Models/
│  ├─ ProductSQLite.cs
│  └─ ProductMongo.cs
├─ Repository/
│  ├─ ProductRepository.cs
│  ├─ SQLiteRepository.cs
│  └─ MongoDBRepository.cs
├─ Service/
│  └─ ProductService.cs
├─ UIs/
│  └─ MenuService.cs
└─ Program.cs
```

### Fördelar med Projektstrukturen

1. **Enkel Underhållbarhet**: Tydligt ansvar per klass gör felsökning och uppdateringar enklare.
2. **Bättre Återanvändbarhet**: Mindre, specialiserade klasser förenklar återanvändning av kod.
3. **Förbättrad Testbarhet**: Användning av interfaces och separata lager gör enhetstester mer isolerade.
4. **Lättare Att Utvidga**: Med en välstrukturerad arkitektur blir det enkelt att lägga till nya funktioner eller datakällor.

## Sammanfattning av Arbetet i Projektet

### 1. Teknisk Implementation

- **SOLID-principer**: Single Responsibility (SRP) och Dependency Inversion (DIP) tillämpas.  
- **Databas**: SQLite används för lokal lagring.  
- **Designmönster**: Repository- och Facade-pattern tillämpas för bättre separation av ansvar.  
- **Utmaningar**: Hantering av dubblettnycklar i MongoDB samt integration av två databaser löstes med validering och gemensamma gränssnitt.

### 2. Arbetsprocess

- **Planering**: Funktioner definierades och bröts ner i mindre delar.  
- **Val av Teknik**: SQLite för enkel lokal datalagring.  
- **Lärdomar**: Ökat fokus på tydlig kodstruktur och SOLID-principer.  
- **Framtida Förbättringar**: Mer fokus på enhetstester och robust migrationshantering.

### 3. Kod-exempel

**IProductBase Interface** - Grundläggande egenskaper för en produkt:  
```csharp
namespace HenriksHobbyLager.Interfaces
{
    public interface IProductBase
    {
        string Name { get; set; }
        decimal Price { get; set; }
        int Stock { get; set; }
    }
}
```

**ProductFacade** - Enkel åtkomst till produktfunktionalitet:  
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

**Repository Pattern** - Isolerar databaslogik:  
```csharp
public void Create(Product product)
{
    _context.Products.Add(product);
    _context.SaveChanges();
}
```

**Interfaces** - Gör koden flexibel och testbar:  
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

## Sammanfattning

HenriksHobbyLager demonstrerar hur tydlig struktur, SOLID-principer och designmönster kan leda till ett lättunderhållet, utbyggbart och testbart system. Genom att separera affärslogik, datatillgång och användargränssnitt blir koden mer överskådlig och flexibel. Detta projekt har gett värdefulla insikter i hur man bygger robusta applikationer med fokus på kvalitet, återanvändbarhet och underhållbarhet.
```