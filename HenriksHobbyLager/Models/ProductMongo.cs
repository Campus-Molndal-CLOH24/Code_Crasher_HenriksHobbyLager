using HenriksHobbyLager.Interfaces;

namespace HenriksHobbyLager.Models
{
    public class ProductMongo : IProductBase
    {
        public string? Id { get; set; } // Låt detta vara null, så skapas det automatiskt av MongoDB
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, Namn: {Name}, Pris: {Price:C}, Lager: {Stock}, Kategori-ID: {CategoryId}";
        }

    }
}