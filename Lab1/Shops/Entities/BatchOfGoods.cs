namespace Shops.Entities;

public class BatchOfGoods
{
    public BatchOfGoods(int productId, int productNumber)
    {
        ProductId = productId;
        ProductNumber = productNumber;
    }

    public int ProductId { get; }
    public int ProductNumber { get; }
}