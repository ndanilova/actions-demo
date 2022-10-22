using Shops.Entities;
using Shops.Exceptions;
using Xunit;

namespace Shops.Test;

public class ShoppingServiceTest
{
    private readonly ShopManager _shopManager = new ShopManager();

    [Fact]

    public void MakeSupply_ShopContainsProducts()
    {
        var shop = _shopManager.CreateShop("Target", "Homan Ave", 2126);
        var productBread = _shopManager.RegisterProduct("Bread", 30);
        var productMilk = _shopManager.RegisterProduct("Milk", 100);

        shop.MakeSupply(new SupplyData(1, 35, 15), new SupplyData(2, 110, 40));

        Assert.Equal("Bread", shop.FindProduct(1).ProductName);
        Assert.Equal("Milk", shop.FindProduct(2).ProductName);
    }

    [Fact]

    public void ChangePrice_PriceChanged()
    {
        const int newPrice = 200;

        var shop = _shopManager.CreateShop("Target", "Homan Ave", 2126);
        var productMilk = _shopManager.RegisterProduct("Milk", 100);

        shop.MakeSupply(new SupplyData(1, 35, 15));

        shop.ChangePriceForProduct(1, newPrice);

        Assert.Equal(newPrice, shop.FindProduct(1).Price);
    }

    [Fact]

    public void FindShopWithTheCheapestProduct_ShopWasFound()
    {
        var target = _shopManager.CreateShop("Target", "Homan Ave", 2126);
        var magnit = _shopManager.CreateShop("Magnit", "Pobedy", 5);
        var diksi = _shopManager.CreateShop("Diksi", "Lenina", 40);
        var productBread = _shopManager.RegisterProduct("Bread", 30);

        target.MakeSupply(new SupplyData(1, 50, 15));
        magnit.MakeSupply(new SupplyData(1, 20, 15));

        var result = _shopManager.GetShopWithCheapestProduct(1);

        Assert.Equal(magnit, result);
    }

    [Fact]

    public void MakeShipment_MoneyIsWrittenOfAndNumberOfProductHasBeenChanged()
    {
        const int moneyBefore = 25000;
        const int breadPrice = 50;
        const int milkPrice = 100;
        const int oilPrice = 400;
        const int productNumber = 100;
        const int shipmentNumber = 40;
        var person = new Customer("Evgeny", moneyBefore);
        var shop = _shopManager.CreateShop("Target", "Homan Ave", 2126);
        var productBread = _shopManager.RegisterProduct("Bread", 30);
        var productMilk = _shopManager.RegisterProduct("Milk", 100);
        var productOliveOil = _shopManager.RegisterProduct("Olive oil", 300);

        shop.MakeSupply(new SupplyData(1, breadPrice, productNumber), new SupplyData(2, milkPrice, productNumber), new SupplyData(3, oilPrice, productNumber));

        shop.MakeShipment(person, new BatchOfGoods(1, shipmentNumber), new BatchOfGoods(2, shipmentNumber), new BatchOfGoods(3, shipmentNumber));
        Assert.Equal(moneyBefore - (shipmentNumber * (breadPrice + milkPrice + oilPrice)), person.Money);
        Assert.Equal(productNumber - shipmentNumber, shop.FindProduct(1).ProductNumber);
    }

    [Fact]

    public void BuyMoreProductsThanShopHas_ThrowException()
    {
        var person = new Customer("Evgeny", 5000);
        var target = _shopManager.CreateShop("Target", "Homan Ave", 2126);
        var productBread = _shopManager.RegisterProduct("Bread", 30);
        target.MakeSupply(new SupplyData(1, 40, 100));

        Assert.Throws<ShopException>(() => target.MakePurchase(person, 1, 200));
    }

    [Fact]

    public void MakePurchaseWithLackOfMoney_TrowException()
    {
        var person = new Customer("Evgeny", 500);
        var target = _shopManager.CreateShop("Target", "Homan Ave", 2126);
        var productBread = _shopManager.RegisterProduct("Bread", 50);
        target.MakeSupply(new SupplyData(1, 40, 100));

        Assert.Throws<ShopException>(() => target.MakePurchase(person, 1, 20));
    }
}