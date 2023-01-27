using Shops.Entities;
using Shops.Services;
using Xunit;

namespace Shops.Test;

public class ShopManagerTest
{
    private ShopManager _shopManager = new ShopManager();

    [Fact]
    public void CreateShopAddItems_ShopContainsItems()
    {
        var shop = _shopManager.AddShop("Uralskya, 75", "Krasnoye beloe");
        var apple = _shopManager.AddItem(new ItemName("apple"), 5, 100.15M);
        var delivery = new List<ItemInShop>();
        delivery.Add(apple);
        shop.AddItem(delivery);
        Assert.NotEmpty(shop.Items);
    }

    [Fact]
    public void AddPersonWithNotEnoughMoneyTryToBuyItem_ThrowException()
    {
    }

    [Fact]
    public void ChangeItemsCost_ItemHasNewCost()
    {
    }

    [Fact]
    public void PersonBuyItem_PersonHasFewMoney()
    {
    }

    [Fact]
    public void CreateShopWichAlready_ThrowException()
    {
    }
}