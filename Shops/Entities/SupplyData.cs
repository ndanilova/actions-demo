using Shops.Exceptions;

namespace Shops.Entities;

public class SupplyData
{
    public SupplyData(int productId, int productPrice, int productNumber)
    {
        if (productId < 0 || productPrice < 0 || productNumber < 0)
            throw new SupplyDataException("Invalid data to parameter given");
        ProductId = productId;
        ProductPrice = productPrice;
        ProductNumber = productNumber;
    }

    public int ProductId { get; }
    public int ProductPrice { get; }
    public int ProductNumber { get; }
}