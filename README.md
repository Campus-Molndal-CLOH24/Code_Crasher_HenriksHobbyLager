```markdown
# HenriksHobbyLager

<span style="color:#00BFFF;">**HenriksHobbyLager**</span> är ett hobbyprojekt byggt i **C#** för att simulera lagerhantering av hobbyprodukter. Projektet använder en kombination av <span style="color:#FFA07A;">**SQLite**</span> och <span style="color:#FF8C00;">**MongoDB**</span> för datalagring, samt erbjuder ett menybaserat konsolgränssnitt för interaktion. Syftet är att visa upp tydlig arkitektur, SOLID-principer och designmönster för en enkel, underhållbar och skalbar kodbas.

## ✨ Funktioner

- <span style="color:#32CD32;">**CRUD-operationer**</span> (Skapa, Läs, Uppdatera, Ta bort produkter)
- <span style="color:#FF69B4;">Sökning</span> efter produkter baserat på kategori
- Val mellan två databaser: <span style="color:#FFA07A;">SQLite</span> och <span style="color:#FF8C00;">MongoDB</span>
- Konsolbaserat, enkelt menygränssnitt

## 🚀 Installation

1. Klona repo:t  
   ```bash
   git clone <https://github.com/Campus-Molndal-CLOH24/Code_Crasher_HenriksHobbyLager>
   ```

2. Installera beroenden:  
   - `Microsoft.EntityFrameworkCore.Sqlite`
   - `MongoDB.Driver`

3. Bygg och kör i Visual Studio (F5)

## 🛠 Hur man kör programmet

Starta applikationen och följ menyerna för att visa, lägga till, uppdatera eller ta bort produkter. Du kan även söka baserat på kategori. Använd piltangenter och siffror för att navigera genom alternativen.

## 📦 Databasstruktur

**SQLite (Products-tabell)**  
- Id (INT, primärnyckel)  
- Name (TEXT)  
- Price (REAL)  
- Stock (INT)  
- CategoryId (INT)

**MongoDB**  
- Liknande fält som i SQLite, men lagras som dokument.

## 📂 Projektstruktur

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

## 🎨 Designmönster & SOLID

- <span style="color:#00CED1;">**Repository Pattern**</span> för att separera dataåtkomstlogik.  
- <span style="color:#DC143C;">**Facade Pattern**</span> för enklare åtkomst till produktoperationer.  
- SOLID-principer (särskilt SRP & DIP) för bättre struktur och testbarhet.

## 💻 Kodexempel

**IProductBase Interface**
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

**ProductFacade**
```csharp
public class ProductFacade : IProductFacade
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

**CRUD i Repository**
```csharp
public void Create(Product product)
{
    _context.Products.Add(product);
    _context.SaveChanges();
}
```

## 🎯 Lärdomar & Framtida Förbättringar

- Ökad tydlighet och struktur gör koden lättare att underhålla.
- Att använda separerade lager och interfaces möjliggör enklare testning.
- I framtiden: mer automatiserade tester, förbättrad migrationshantering och ytterligare valideringar.

## 📜 Licens

Projektet licensieras under [MIT License](./LICENSE).

---

<span style="color:#6A5ACD;">Tack för att du kikar på HenriksHobbyLager! 🎉</span>
```