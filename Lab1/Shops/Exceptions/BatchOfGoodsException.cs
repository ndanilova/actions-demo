namespace Shops.Exceptions;

public class BatchOfGoodsException : Exception
{
    public BatchOfGoodsException(string message)
        : base(message)
    {
    }
}