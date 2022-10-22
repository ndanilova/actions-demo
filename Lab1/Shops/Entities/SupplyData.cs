namespace Shops.Entities;

public class SupplyData
{
    public SupplyData(int productId, int productPrice, int productNumber)
    {
        ProductId = productId;
        ProductPrice = productPrice;
        ProductNumber = productNumber;
    }

    public int ProductId { get; }
    public int ProductPrice { get; }
    public int ProductNumber { get; }
}