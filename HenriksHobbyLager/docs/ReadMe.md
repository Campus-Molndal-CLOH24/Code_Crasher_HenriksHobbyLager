# HenriksHobbyLager

HenriksHobbyLager �r ett hobbyprojekt som jag har byggt f�r att simulera lagerhantering av hobbyprodukter. Projektet �r implementerat i C# och anv�nder en kombination av SQLite och MongoDB f�r att hantera lagring av data. Systemet erbjuder ett menybaserat konsolgr�nssnitt f�r att visa, l�gga till, uppdatera, ta bort produkter, samt s�ka efter produkter utifr�n kategori.

## Installation
F�r att komma ig�ng med HenriksHobbyLager, f�lj dessa steg:

1. Klona det h�r repo:t till din lokala maskin.
   ```sh
   git clone <repo-url>
   ```
2. Installera n�dv�ndiga beroenden via NuGet:
   - `MongoDB.Driver` f�r anslutning till MongoDB
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
- **MongoDB Databas**: MongoDB konfigureras f�r att arbeta med `MongoDbContext`. Du kan anpassa MongoDB-anslutningen genom att justera anslutningsstr�ngen i `MongoDbContext` (default: `mongodb://localhost:27017`).

## Implementerade Patterns
- **Repository Pattern**: F�r att hantera �tkomst till databasen och CRUD-operationer anv�nds `ProductRepository` f�r SQLite och `MongoDBRepository` f�r MongoDB. Dessa klasser fungerar som en abstraktion mellan databasen och den �vriga aff�rslogiken.
- **Facade Pattern**: `ProductFacade` anv�nds f�r att kapsla in komplexiteten med att anropa olika repository-klasser och f�renkla �tkomst till produktrelaterade operationer.

## Databasstruktur
- **SQLite (Produktlagring)**: En databas vid namn `HenriksHobbyLager.db` anv�nds f�r lokal lagring av produkter. Tabellen `Products` inneh�ller kolumnerna:
  - `Id`: Prim�rnyckel (INT)
  - `Name`: Produktnamn (TEXT)
  - `Price`: Pris p� produkten (REAL)
  - `Stock`: Antal i lager (INT)
  - `CategoryId`: ID f�r tillh�rande kategori (INT)
- **MongoDB (Produktlagring)**: MongoDB anv�nds f�r att l�gga till och h�mta produkter fr�n databasen `HenriksHobbyLager`, d�r produkterna lagras i en samling med namnet `Products`. Varje produkt f�r ett unikt `ObjectId`.

## Sammanfattning av Arbetet i Projektet
I detta projekt har jag arbetat med att skapa en applikation f�r lagerhantering som anv�nder tv� olika databaser � SQLite f�r lokal datalagring och MongoDB f�r molnbaserad datalagring. Projektet �r uppdelat f�r att hantera dessa tv� typer av lagring separat via dedikerade `Repository`-klasser och kontexter. B�da databaserna hanteras via deras respektive repository-klasser som implementerar CRUD-operationer, och `ProductFacade` hj�lper till att sammanf�ra alla operationer. Dessutom anv�nder jag ett konsolgr�nssnitt f�r att f� feedback p� hur systemet fungerar i praktiken, och hur data synkroniseras mellan databaserna. Detta arbetss�tt har gett mig en f�rst�else f�r integration av olika lagringsteknologier samt till�mpning av vanliga designm�nster.

Jag har st�tt p� utmaningar som `DuplicateKey`-fel vid ins�ttning i MongoDB, men jag har inte helt lyckats l�sa detta problem utan att orsaka andra fel i systemet. Detta �r ett omr�de som fortfarande kr�ver mer arbete och anpassning f�r att hantera `ObjectId` p� ett smidigare s�tt. Slutligen har jag anpassat `Product`-klassen s� att den fungerar korrekt b�de f�r MongoDB och SQLite, genom att skapa separata modeller f�r de tv� databaserna.

