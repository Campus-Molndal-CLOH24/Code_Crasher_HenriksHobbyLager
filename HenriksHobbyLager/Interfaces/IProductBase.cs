namespace HenriksHobbyLager.Interfaces
{


public interface IProductBase
{
    string Name { get; set; }
    decimal Price { get; set; }
    int Stock { get; set; }
}

}