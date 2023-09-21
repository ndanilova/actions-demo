namespace Shops.Exceptions;

public class NullCustomerException : Exception
{
    public NullCustomerException(string message)
        : base(message)
    {
    }
}