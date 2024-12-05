```markdown
# ✨ HenriksHobbyLager

**HenriksHobbyLager** är ett C#-baserat hobbyprojekt som simulerar lagerhantering av hobbyprodukter. Med en kombination av **SQLite** och **MongoDB** som datalager, 
och ett enkelt menygränssnitt i konsolen, visas hur SOLID-principer och designmönster kan skapa en tydlig, flexibel och testbar kodbas.

## 🌱 Funktioner

- **CRUD-operationer:** Skapa, visa, uppdatera och ta bort produkter.
- **Kategori-baserad sökning:** Hitta produkter utifrån kategori.
- **Flera datalager:** Hantera produkter i både SQLite och MongoDB.
- **Konsolbaserat gränssnitt:** Enkelt menyval för att interagera med lagret.

## 🚀 Komma igång

1. **Klona projektet:**
   ```bash
   git clone <repo-url>
   ```

2. **Installera beroenden (exempel):**
   - `Microsoft.EntityFrameworkCore.Sqlite`
   - `MongoDB.Driver`

3. **Öppna i IDE:**
   Öppna projektet i Visual Studio eller Visual Studio Code och kontrollera att alla paket är installerade.

4. **Bygg och kör (F5):**
   När applikationen startar visas en meny där du kan hantera produkter och söka utifrån kategori.

## 💾 Databas & Strukturer

- **SQLite:**  
  En lokal filbaserad databas med tabellen `Products` (Id, Name, Price, Stock, CategoryId).
  
- **MongoDB:**  
  Lagrar produkterna som dokument med motsvarande egenskaper som i SQLite, men i ett dokumentorienterat format.

## 🗂 Projektstruktur

```bash
HenriksHobbyLager/
├─ Database/
│  ├─ SqliteDbContext.cs       # SQLite-konfiguration
│  └─ MongoDbContext.cs        # MongoDB-konfiguration
├─ Facade/
│  └─ ProductFacade.cs         # Fasad för förenklad åtkomst
├─ Interfaces/
│  ├─ IProductRepository.cs    # Kontrakt för datalagring
│  ├─ IProductFacade.cs        # Kontrakt för fasaden
│  └─ IProductBase.cs          # Grundegenskaper för produkt
├─ Models/
│  ├─ ProductSQLite.cs         # Modell för SQLite-produkt
│  └─ ProductMongo.cs          # Modell för MongoDB-produkt
├─ Repository/
│  ├─ ProductRepository.cs     # Generisk datalagerlogik
│  ├─ SQLiteRepository.cs      # Specifik för SQLite
│  └─ MongoDBRepository.cs     # Specifik för MongoDB
├─ Service/
│  └─ ProductService.cs        # Affärslogik
├─ UIs/
│  └─ MenuService.cs           # Konsolmeny för användarinteraktion
└─ Program.cs                  # Programstartpunkt
```

## 🧩 Designmönster & Principer

- **Repository Pattern:** Isolerar dataåtkomst från affärslogik för bättre struktur och testbarhet.
- **Facade Pattern:** Erbjuder en förenklad yta för komplexa operationer, vilket minskar beroenden i koden.
- **SOLID-principer:** Särskilt Single Responsibility & Dependency Inversion, för att skapa modulär, robust och lättunderhållen kod.

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

- **Underhållbarhet:** Tydlig ansvarsuppdelning gör det enkelt att ändra och förbättra koden över tid.
- **Testbarhet:** Med interfaces och separata lager kan funktioner testas isolerat.
- **Framtida förbättringar:** Mer avancerad validering, fler enhetstester och smartare migrationshantering för databaser.

## 📜 Licens

Detta projekt är tillgängligt under [MIT License](./LICENSE).

---

**Tack för att du besöker HenriksHobbyLager!** Om du har förslag, frågor eller vill bidra, tveka inte att skapa ett issue eller en pull request.