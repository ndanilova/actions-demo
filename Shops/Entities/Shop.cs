using Shops.Exceptions;

namespace Shops.Entities;

public class Shop
{
    public Shop(string name, int id, string street, int buildingNumber)
    {
        if (string.IsNullOrWhiteSpace(name))
            ShopManagerException.InvalidShopName();
        if (string.IsNullOrWhiteSpace(street))
            ShopManagerException.InvalidStreet();
        if (buildingNumber <= 0)
            ShopManagerException.InvalidBuildingNumber();
        Id = id;
        Street = street;
        BuildingNumber = buildingNumber;
        ProductByProductId = new Dictionary<int, Product>();
        Name = name;
    }

    public int Id { get;  }
    public string Name { get; }
    public int Revenue { get; private set; }

    public string FullAddress => $"{Street}, {BuildingNumber}";
    private string Street { get; }
    private int BuildingNumber { get; }

    private Dictionary<int, Product> ProductByProductId { get; }

    public void MakeSupply(params SupplyData[] supplyList)
    {
        if (supplyList.Length == 0)
            ShopException.CantMakeSupplyWithEmptyList();
        foreach (var supplyData in supplyList)
        {
            MakeSupply(supplyData.ProductId, supplyData.ProductNumber, supplyData.ProductPrice);
        }
    }

    public void MakeShipment(Customer customer, params BatchOfGoods[] batchOfGoodsArray)
    {
        if (batchOfGoodsArray.Length == 0)
            ShopException.CantMakeShipmentWithEmptyList();
        foreach (var batchOfGoods in batchOfGoodsArray)
        {
            MakePurchase(customer, batchOfGoods.ProductId, batchOfGoods.ProductNumber);
        }
    }

    public void MakePurchase(Customer customer, int productId, int number)
    {
        if (customer == null)
            throw new NullCustomerException("Invalid customer");
        if (!TryFindProduct(productId))
            ShopException.WrongId(productId);
        if (ProductByProductId[productId].ProductNumber < number)
            ShopException.LackOfProduct();
        int cost = ProductByProductId[productId].Price * number;
        if (customer.Money < cost)
            ShopException.NotEnoughMoney();
        customer.Money -= cost;
        Revenue += cost;
        ProductByProductId[productId].ProductNumber -= number;
    }

    public Product RegisterProductInThisShop(string productName, int id, int price)
    {
        if (string.IsNullOrWhiteSpace(productName))
            ShopException.InvalidProductName();
        if (price <= 0)
            ShopException.InvalidPrice();
        var product = new Product(productName, id, price);
        ProductByProductId.Add(product.Id, product);
        return product;
    }

    public void ChangePriceForProduct(int prductId, int newPrice)
    {
        if (!TryFindProduct(prductId))
            ShopException.WrongId(prductId);
        if (newPrice <= 0)
            ShopException.InvalidPrice();
        ProductByProductId[prductId].Price = newPrice;
    }

    public Product FindProduct(int productId)
    {
        if (!TryFindProduct(productId))
            ShopException.WrongId(productId);
        var result = ProductByProductId[productId];
        var copy = new Product(result.ProductName, result.Id, result.Price)
        {
            ProductNumber = result.ProductNumber,
        };
        return copy;
    }

    private void MakeSupply(int productId, int number, int price)
    {
        if (!TryFindProduct(productId))
            ShopException.WrongId(productId);
        if (number <= 0)
            ShopException.InvalidNumber();
        if (price <= 0)
            ShopException.InvalidPrice();
        if (ProductByProductId[productId].Price != price)
            ProductByProductId[productId].Price = price;
        ProductByProductId[productId].ProductNumber = number;
    }

    private bool TryFindProduct(int productId)
    {
        return ProductByProductId.ContainsKey(productId);
    }
}