using System;
using HenriksHobbyLager.Repository;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Facade;

namespace HenriksHobbyLager.Models;

public class Product
 {
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? Category { get; set; }
    public DateTime Created { get; set; }
    public DateTime? LastUpdated { get; set; }  // it means the property can hold a null value. 
    public int CategoryId { get; internal set; }
}


