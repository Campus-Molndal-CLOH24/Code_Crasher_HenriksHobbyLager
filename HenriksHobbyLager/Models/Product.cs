using System;
namespace HenriksHobbyLager.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HenriksHobbyLager.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

[Table("Products")]
public class Product 
 {
    
    [Key]
    public int Id { get; set; }

    // Use this property for MongoDB
    [BsonId]
    [NotMapped]
    public int MongoId { get; set; } // MongoDB's default _id type
    
    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? Category { get; set; }
    public DateTime? Created { get; set; }
    public DateTime? LastUpdated { get; set; }  // it means the property can hold a null value.

    //this is a concurrency token
    [ConcurrencyCheck]
    public byte[]? RowVersion { get; set; }

    

    
}


