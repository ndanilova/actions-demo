namespace Shops.Exceptions;

public class ShopException : Exception
{
    private ShopException(string message)
        : base(message)
    {
    }

    public static ShopException CantMakeSupplyWithEmptyList()
    {
        throw new ShopException("Supply list can't be empty");
    }

    public static ShopException CantMakeShipmentWithEmptyList()
    {
        throw new ShopException("Shipment list can't be empty");
    }

    public static ShopException InvalidPrice()
    {
        throw new ShopException("Price must be greater than 0");
    }

    public static ShopException InvalidNumber()
    {
        throw new ShopException("Number must be greater than 0");
    }

    public static ShopException InvalidProductName()
    {
        throw new ShopException("Invalid product name");
    }

    public static ShopException WrongId(int id)
    {
        throw new ShopException($"Product with {id} not found");
    }

    public static ShopException InvalidCustomerData()
    {
        throw new ShopException("Invalid customer");
    }

    public static ShopException LackOfProduct()
    {
        throw new ShopException("Shop doesn't have needed number of product");
    }

    public static ShopException NotEnoughMoney()
    {
        throw new ShopException("Customer doesn't have enough money");
    }
}