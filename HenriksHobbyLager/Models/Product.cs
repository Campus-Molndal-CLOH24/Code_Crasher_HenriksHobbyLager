using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

/*public class Product
 {
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string Category { get; set; }
    public DateTime Created { get; set; }
    public DateTime? LastUpdated { get; set; }  // it means the property can hold a null value. 
}*/


// Definiera modellen Product
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string Category { get; set; }
    public DateTime Created { get; set; }
    public DateTime? LastUpdated { get; set; } // Nullable för att tillåta null-värden
}

