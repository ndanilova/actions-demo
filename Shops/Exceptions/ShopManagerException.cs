namespace Shops.Exceptions;

public class ShopManagerException : Exception
{
    public ShopManagerException(string message)
        : base(message)
    {
    }

    public static ShopManagerException NoShopsYet()
    {
        throw new ShopManagerException("No shops created yet");
    }

    public static ShopManagerException InvalidPrice()
    {
        throw new ShopManagerException("Price must be greater than 0");
    }

    public static ShopManagerException InvalidShopName()
    {
        throw new ShopManagerException("Invalid name");
    }

    public static ShopManagerException InvalidStreet()
    {
        throw new ShopManagerException("Invalid street");
    }

    public static ShopManagerException InvalidBuildingNumber()
    {
        throw new ShopManagerException("Building number must be greater than 0");
    }

    public static ShopManagerException InvalidProductName()
    {
        throw new ShopManagerException("Invalid product name");
    }
}