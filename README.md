# ✨ HenriksHobbyLager

**HenriksHobbyLager** är ett C#-baserat hobbyprojekt som demonstrerar hur man kan bygga en lagerhanteringsapplikation för hobbyprodukter. Projektet använder **SQLite** och **MongoDB** för datalagring, samt ett enkelt konsolbaserat menygränssnitt. Genom att följa **SOLID-principer** och använda välkända designmönster som **Repository** och **Facade** skapas en flexibel, testbar och lättunderhållen kodbas.

## 🌱 Funktioner

- **CRUD-operationer:** Skapa, läs, uppdatera och ta bort produkter.
- **Sökning per kategori:** Hitta produkter baserat på kategori.
- **Flera datalager:** Hantera produkter både i SQLite och MongoDB.
- **Konsolgränssnitt:** Enkel meny för interaktion med lagret.

## 🚀 Kom igång

1. **Klona projektet:**
   ```bash
   git clone <repo-url>
   ```
   
2. **Installera beroenden:**  
   Säkerställ att följande paket finns:
   - Microsoft.EntityFrameworkCore.Sqlite
   - MongoDB.Driver
   
3. **Öppna i IDE:**  
   Öppna projektet i Visual Studio eller Visual Studio Code och kontrollera att alla beroenden är installerade.

4. **Bygg & kör:**  
   När projektet körs (t.ex. F5 i Visual Studio) visas en meny där du kan utföra CRUD-operationer och sökningar på produkter.

## 💾 Databaser & Struktur

- **SQLite:**  
  En lokal filbaserad databas med en `Products`-tabell (Id, Name, Price, Stock, CategoryId).
  
- **MongoDB:**  
  Produkterna lagras som dokument med motsvarande fält som i SQLite, men i ett dokumentorienterat format.

## 🗂 Projektstruktur

```
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

## 🧩 Designmönster & Principer

- **Repository Pattern:** Abstraherar dataåtkomst från affärslogik och förenklar testning och underhåll.
- **Facade Pattern:** Ger en förenklad gränsyta till komplex funktionalitet, minskar beroenden och förbättrar kodstruktur.
- **SOLID-principer:** Fokus på Single Responsibility och Dependency Inversion skapar modulär, robust och lättunderhållen kod.

## 💻 Kodexempel

**IProductBase Interface:**
```csharp
public interface IProductBase
{
    string Name { get; set; }
    decimal Price { get; set; }
    int Stock { get; set; }
}
```

**ProductFacade:**
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

## 🌟 Lärdomar & Framtid

- **Underhållbarhet:** Kodens tydliga ansvarsfördelning underlättar framtida förbättringar.
- **Testbarhet:** Tack vare interfaces och separata lager kan funktioner testas isolerat.
- **Framtida förbättringar:** Mer avancerad validering, fler enhetstester och bättre migrationshantering för databaserna.

## 📜 Licens

Detta projekt är licensierat under [MIT License](./LICENSE).

---

**Tack för att du tittar på HenriksHobbyLager!**  
Har du förslag, frågor eller vill bidra? Skapa gärna ett issue eller skicka in en pull request.