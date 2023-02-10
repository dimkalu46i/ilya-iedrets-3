using Shops.Entities;
using Shops.Exceptions;
using Shops.Services;
using Xunit;

namespace Shops.Test;

public class ShopManagerTest
{
    private ShopManager _shopManager = new ShopManager();

    [Fact]
    public void CreateShopAddItem_ShopContainsItem()
    {
        Shop shop = _shopManager.AddShop("Уральская, 75", "Красное белое");
        ItemInShop apple = _shopManager.AddItemToShop(shop, new ItemName("яблоко"), 100M);
        Assert.NotEmpty(shop.Items);
    }

    [Fact]
    public void AddPersonWithNotEnoughMoney_ThrowException()
    {
        Assert.Throws<NotEnoughMoneyException>(() =>
            _shopManager.AddPerson("Ivan", 0));
    }

    [Fact]
    public void ChangeItemCost_ItemHasNewCost()
    {
        Shop shop = _shopManager.AddShop("Уральская, 75", "Красное белое");
        ItemInShop apple = _shopManager.AddItemToShop(shop, new ItemName("яблоко"), 100M);
        apple.ChangeCost(101M);
        const decimal expectedCost = 101M;
        Assert.Equal(expectedCost, apple.Cost);
    }

    [Fact]
    public void PersonBuyItem_PersonHasFewMoney_ThrowException()
    {
        Person person = _shopManager.AddPerson("Ivan", 5M);
        Shop shop = _shopManager.AddShop("Уральская, 75", "Красное белое");
        ItemInShop apple = _shopManager.AddItemToShop(shop, new ItemName("яблоко"), 100M);
        Order order = _shopManager.AddOrderToPerson(shop, person);
        Order updatedOrder = _shopManager.AddItemsToOrder(shop, person, order.Id, apple);
        Assert.Throws<NotEnoughMoneyException>(() => _shopManager.PayOrder(person, updatedOrder.Id));
    }

    [Fact]
    public void CreateShopWichAlreadyExists_ThrowException()
    {
        Shop shop = _shopManager.AddShop("Уральская, 75", "Красное белое");
        Assert.Throws<ShopManagerException>(() => _shopManager.AddShop("Уральская, 75", "Красное белое"));
    }

    [Fact]
    public void AddOrderToPerson_PersonBuyOrder()
    {
        Person person = _shopManager.AddPerson("Ivan", 100M);
        Shop shop = _shopManager.AddShop("Уральская, 75", "Красное белое");
        ItemInShop apple = _shopManager.AddItemToShop(shop, new ItemName("яблоко"), 100M);
        Order order = _shopManager.AddOrderToPerson(shop, person);
        _shopManager.AddItemsToOrder(shop, person, order.Id, apple);

        Assert.Equal(order, _shopManager.PayOrder(person, order.Id));
    }

    [Fact]
    public void FindTheCheapestItem_FoundItemIsTheCheapest()
    {
        Shop? shop_1 = _shopManager.AddShop("Уральская, 75", "Красное белое");
        Shop shop_2 = _shopManager.AddShop("Уральская, 67", "Кировский");
        ItemName apple = new ItemName("яблоко");
        ItemInShop item_1 = _shopManager.AddItemToShop(shop_1, apple, 100M);
        ItemInShop item_2 = _shopManager.AddItemToShop(shop_1, apple, 200M);
        _shopManager.AddItemToShop(shop_2, item_2.ItemName, item_2.Cost);
        _shopManager.AddItemToShop(shop_1, item_1.ItemName, item_1.Cost);
        Assert.Equal(shop_1, _shopManager.GetShopWithBestOffer(apple));
    }
}