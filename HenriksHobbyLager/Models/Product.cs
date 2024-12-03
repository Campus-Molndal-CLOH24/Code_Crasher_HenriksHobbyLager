using System;
using HenriksHobbyLager.Repository;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Facade;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HenriksHobbyLager.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? Category { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastUpdated { get; set; }
        public int CategoryId { get; set; }
    }
}

