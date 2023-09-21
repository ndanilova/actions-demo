using Shops.Exceptions;

namespace Shops.Entities;

public class Product
{
    private int _productNumber;
    public Product(string name, int id, int price)
    {
        if (string.IsNullOrWhiteSpace(name) || id < 0 || price < 0)
            throw new ShopException("Invalid parameter to Product constructor");
        ProductName = name;
        Id = id;
        Price = price;
    }

    public int Price { get; set; }
    public int Id { get; }
    public string ProductName { get; }

    public int ProductNumber
    {
        get => _productNumber;
        set
        {
            if (value < 0)
                ShopException.InvalidNumber();
            _productNumber = value;
        }
    }
}