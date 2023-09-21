using Shops.Exceptions;

namespace Shops.Entities;

public class Customer
{
    private int _money;
    public Customer(string name, int money)
    {
        if (string.IsNullOrWhiteSpace(name) || money < 0)
            throw new CustomerException("Invalid parameter to constructor");
        Name = name;
        Money = money;
    }

    public string Name { get; }

    public int Money
    {
        get => _money;
        set
        {
            if (value < 0)
                throw new CustomerException("number of money can't be less than zero");
            _money = value;
        }
    }
}