using System;

public class Menu
{
    static void Main(string[] args)
    {
        // Huvudloopen - Stäng inte av programmet, då försvinner allt!
        while (true)
        {
            Console.Clear();  // Rensar skärmen så det ser proffsigt ut
            Console.WriteLine("=== Henriks HobbyLager™ 2.0 ===");
            Console.WriteLine("1. Visa alla produkter");
            Console.WriteLine("2. Lägg till produkt");
            Console.WriteLine("3. Uppdatera produkt");
            Console.WriteLine("4. Ta bort produkt");
            Console.WriteLine("5. Sök produkter");
            Console.WriteLine("6. Avsluta");  // Använd inte denna om du vill behålla datan!

            var choice = Console.ReadLine();

            // Switch är tydligen bättre än if-else enligt Google
            switch (choice)
            {
                case "1":
                    ShowAllProducts();
                    break;
                case "2":
                    AddProduct();
                    break;
                case "3":
                    UpdateProduct();
                    break;
                case "4":
                    DeleteProduct();
                    break;
                case "5":
                    SearchProducts();
                    break;
                case "6":
                    return;  // OBS! All data försvinner om du väljer denna!
                default:
                    Console.WriteLine("Ogiltigt val! Är du säker på att du tryckte på rätt knapp?");
                    break;
            }

            Console.WriteLine("\nTryck på valfri tangent för att fortsätta... (helst inte ESC)");
            Console.ReadKey();
        }
        
}
