namespace Shops.Entities;

public class Product
{
    public Product(string name, int id, int price)
    {
        ProductName = name;
        Id = id;
        Price = price;
    }

    public int Price { get; set; }
    public int Id { get; }
    public string ProductName { get; }

    public int ProductNumber { get; set; }
}