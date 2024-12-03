# HenriksHobbyLager

HenriksHobbyLager är ett hobbyprojekt som jag har byggt för att simulera lagerhantering av hobbyprodukter. Projektet är implementerat i C# och använder en kombination av SQLite och MongoDB för att hantera lagring av data. Systemet erbjuder ett menybaserat konsolgränssnitt för att visa, lägga till, uppdatera, ta bort produkter, samt söka efter produkter utifrån kategori.

## Installation
För att komma igång med HenriksHobbyLager, följ dessa steg:

1. Klona det här repo:t till din lokala maskin.
   ```sh
   git clone <repo-url>
   ```
2. Installera nödvändiga beroenden via NuGet:
   - `MongoDB.Driver` för anslutning till MongoDB
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
- **MongoDB Databas**: MongoDB konfigureras för att arbeta med `MongoDbContext`. Du kan anpassa MongoDB-anslutningen genom att justera anslutningssträngen i `MongoDbContext` (default: `mongodb://localhost:27017`).

## Implementerade Patterns
- **Repository Pattern**: För att hantera åtkomst till databasen och CRUD-operationer används `ProductRepository` för SQLite och `MongoDBRepository` för MongoDB. Dessa klasser fungerar som en abstraktion mellan databasen och den övriga affärslogiken.
- **Facade Pattern**: `ProductFacade` används för att kapsla in komplexiteten med att anropa olika repository-klasser och förenkla åtkomst till produktrelaterade operationer.

## Databasstruktur
- **SQLite (Produktlagring)**: En databas vid namn `HenriksHobbyLager.db` används för lokal lagring av produkter. Tabellen `Products` innehåller kolumnerna:
  - `Id`: Primärnyckel (INT)
  - `Name`: Produktnamn (TEXT)
  - `Price`: Pris på produkten (REAL)
  - `Stock`: Antal i lager (INT)
  - `CategoryId`: ID för tillhörande kategori (INT)
- **MongoDB (Produktlagring)**: MongoDB används för att lägga till och hämta produkter från databasen `HenriksHobbyLager`, där produkterna lagras i en samling med namnet `Products`. Varje produkt får ett unikt `ObjectId`.

## Sammanfattning av Arbetet i Projektet
I detta projekt har jag arbetat med att skapa en applikation för lagerhantering som använder två olika databaser – SQLite för lokal datalagring och MongoDB för molnbaserad datalagring. Projektet är uppdelat för att hantera dessa två typer av lagring separat via dedikerade `Repository`-klasser och kontexter. Båda databaserna hanteras via deras respektive repository-klasser som implementerar CRUD-operationer, och `ProductFacade` hjälper till att sammanföra alla operationer. Dessutom använder jag ett konsolgränssnitt för att få feedback på hur systemet fungerar i praktiken, och hur data synkroniseras mellan databaserna. Detta arbetssätt har gett mig en förståelse för integration av olika lagringsteknologier samt tillämpning av vanliga designmönster.

Jag har stött på utmaningar som `DuplicateKey`-fel vid insättning i MongoDB, men jag har inte helt lyckats lösa detta problem utan att orsaka andra fel i systemet. Detta är ett område som fortfarande kräver mer arbete och anpassning för att hantera `ObjectId` på ett smidigare sätt. Slutligen har jag anpassat `Product`-klassen så att den fungerar korrekt både för MongoDB och SQLite, genom att skapa separata modeller för de två databaserna.

