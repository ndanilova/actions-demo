namespace Shops.Entities;

public class Customer
{
    public Customer(string name, int money)
    {
        Name = name;
        Money = money;
    }

    public string Name { get; }
    public int Money { get; set; }
}