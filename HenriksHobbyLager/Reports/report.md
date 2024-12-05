# **Individuell Rapport** - Albin Stenhoff

## **Hur fungerade gruppens arbete?**
Grupparbetet fungerade inte optimalt. Vår strategi var att varje gruppmedlem arbetade för sig själv på olika delar av projektet, och sedan skulle vi samla ihop det projekt som var mest färdigt. Tyvärr ledde detta till att jag fick ta hand om en stor del av arbetet, inklusive att lösa problem och skapa den mest färdiga versionen. Det hade varit bättre med tydligare delegering av uppgifter och en gemensam struktur för hur vi skulle arbeta.

---

## **Beskriv gruppens databasimplementation**
Gruppen implementerade en databas med två alternativ: SQLite och MongoDB. Dessa databaser hanterade CRUD-operationer för produkter, med fokus på att skapa, läsa, uppdatera och ta bort poster. 

- **SQLite:** Användes som en relationsdatabas med fördefinierade datamodeller och strikt dataintegritet.
- **MongoDB:** Användes som en dokumentbaserad databas för att hantera mer flexibel data, särskilt för projekt där datamodellerna kunde ändras ofta. Jag implementerade MongoDB genom `MongoDB.Driver` och skapade ett specifikt repository (`MongoDBRepository`) för att hantera dess unika funktionalitet.

---

## **Vilka SOLID-principer implementerade ni och hur?**
Vi försökte följa SOLID-principerna på följande sätt:

1. **Single Responsibility Principle (SRP):** Varje klass hade ett specifikt ansvar. Till exempel hanterade `ProductRepository` databaskommunikation, medan `ProductFacade` hanterade affärslogiken.
2. **Open/Closed Principle (OCP):** Vi skapade `IProductRepository` och `IProductFacade` för att kunna byta ut implementationer utan att behöva ändra befintlig kod. Till exempel kunde SQLite och MongoDB-repositories bytas ut via beroendeinjektion.
3. **Dependency Inversion Principle (DIP):** Vi använde gränssnitt (`IProductRepository` och `IProductFacade`) för att skapa lösa kopplingar mellan olika lager i applikationen.

---

## **Vilka patterns använde ni och varför?**
- **Repository Pattern:** Användes för att abstrahera datalagerlogik och separera den från affärslogiken. Detta gjorde det enklare att byta mellan olika databasimplementationer (SQLite och MongoDB).
- **Facade Pattern:** Användes för att ge en enkel åtkomstpunkt till affärslogiken och dölja komplexiteten i backend-operationer.
- **Dependency Injection:** Användes för att hantera beroenden och göra koden mer testbar och flexibel.

Dessa mönster valdes för att skapa en strukturerad och modulär applikation som var enkel att underhålla och utöka.

---

## **Vilka tekniska utmaningar stötte ni på och hur löste ni dem?**
1. **Duplicate Key Error:** Jag och en annan gruppmedlem (Yotaka) stötte på problem med duplicerade nycklar i MongoDB. Vi löste det genom att verifiera unika ID:n innan vi skapade nya poster i databasen.
2. **Integration av olika repository-implementationer:** Det var en utmaning att integrera både SQLite och MongoDB eftersom de hade olika sätt att hantera data. Vi löste det genom att implementera ett gemensamt gränssnitt (`IProductRepository`) som båda implementationerna följde.
3. **Hantering av MongoDB-specifika problem:** Genom att studera `MongoDB.Driver` och dess unika metoder löste jag problem som rörde fel vid insättning och uppdatering.

---

## **Hur planerade du ditt arbete?**
Jag började med att förstå projektkraven och bryta ner uppgifterna i mindre delar. Därefter prioriterade jag att implementera grundläggande funktionalitet, såsom CRUD-operationer och integration av databaserna. När problemen uppstod, arbetade jag iterativt för att lösa dem och förbättra kodens struktur.

---

## **Vilka delar gjorde du?**
Jag tog ansvar för:
1. Implementationen av `ProductFacade` och dess integration med `IProductRepository`.
2. Lösning av problem med duplicerade nycklar i MongoDB.
3. Testning och felsökning av projektet för att skapa en fungerande version.
4. Integration av SQLite och MongoDB-repositories.
5. Implementering av menytjänsten för att hantera CRUD-operationer på båda databaserna.

---

## **Vilka utmaningar stötte du på och hur löste du dem?**
1. **Duplicate Key Error i MongoDB:** Jag implementerade en kontroll för att verifiera att ID:n var unika innan poster lades till i databasen.
2. **Brist på arbetsfördelning:** Jag tog över och sammanställde koden från olika delar av projektet för att skapa en fungerande helhet.
3. **Hantering av två databaser:** Jag skapade gemensamma gränssnitt för att abstrahera databaslogiken och möjliggöra enkel integration.
4. **Felhantering vid insättning:** Jag lade till specifik felhantering för MongoDB:s unika felmeddelanden, såsom DuplicateKeyException.

---

## **Vad skulle du göra annorlunda nästa gång?**
1. Jag skulle arbeta för att skapa en tydligare struktur för arbetsfördelning i gruppen, så att alla har ett specifikt ansvar.
2. Jag skulle prioritera regelbundna möten för att diskutera framsteg och lösa problem tillsammans.
3. Jag skulle dokumentera kod och beslut bättre för att underlätta samarbete och felsökning.
4. Jag skulle inkludera testdriven utveckling (TDD) för att tidigt identifiera och lösa buggar.

---

Denna rapport reflekterar mina bidrag och erfarenheter från projektet. Anpassa den vidare om det behövs för specifika insatser!
