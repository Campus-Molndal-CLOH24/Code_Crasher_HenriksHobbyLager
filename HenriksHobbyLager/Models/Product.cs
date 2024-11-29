using System;
namespace HenriksHobbyLager.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
[Table("Products")]
public class Product
 {
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? Category { get; set; }
    public DateTime? Created { get; set; }
    public DateTime? LastUpdated { get; set; }  // it means the property can hold a null value.


}


