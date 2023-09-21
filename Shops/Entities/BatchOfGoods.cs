using Shops.Exceptions;

namespace Shops.Entities;

public class BatchOfGoods
{
    public BatchOfGoods(int productId, int productNumber)
    {
        if (productId < 0 || productNumber < 0)
            throw new BatchOfGoodsException("Invalid parameter to constructor");
        ProductId = productId;
        ProductNumber = productNumber;
    }

    public int ProductId { get; }
    public int ProductNumber { get; }
}