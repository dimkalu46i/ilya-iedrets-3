using Shops.Entities;
using Shops.Exceptions;

namespace Shops.Services;

public class ShopManager : IShopManager
{
    private readonly List<Shop> _shops = new ();
    private readonly List<ItemInShop> _items = new ();
    private readonly List<Person> _people = new ();

    public Shop AddShop(string address, string shopName)
    {
        if (_shops.Any(shop => shop.ShopName.Equals(shopName) && shop.Address.Equals(address)))
        {
            throw new ShopManagerException("shop already contains in shops");
        }

        var currentShop = new Shop(address, shopName, new List<ItemInShop>());
        _shops.Add(currentShop);

        return currentShop;
    }

    public ItemInShop AddItem(ItemName itemName, int amount, decimal cost = 0)
    {
        if (_items.Any(item => item.ItemName.Equals(itemName)))
        {
            throw new ShopManagerException("item already contains in items");
        }

        var itemInShop = new ItemInShop(itemName, amount, cost);
        _items.Add(itemInShop);

        return itemInShop;
    }

    public Person AddPerson(string firstName, decimal money)
    {
        if (_people.Any(person => person.FirstName.Equals(firstName)))
        {
            throw new ShopManagerException("person already contains in people");
        }

        var person = new Person(firstName, money);
        _people.Add(person);

        return person;
    }

    // public decimal BuyItems(Person person)
    // {
    //
    //     person.Buy(person.Money);
    //
    // }

    /*public Shop? GetShopWithBestOffer(ItemInShop item)
    {
        decimal bestOffer = new decimal(1000000.0);
        foreach (Shop shop in _shops)
        {
            var currentItem = shop.Items.Find(itemInCurrentShop =>
                itemInCurrentShop.ItemName.Equals(item.ItemName) && itemInCurrentShop.Amount >= item.Amount &&
                itemInCurrentShop.Cost <= bestOffer);
            if (currentItem != null)
            {
                bestOffer = currentItem.Cost;
                var shopWithBestOffer = shop;
            }
            else
            {
                var shopWit
            }
        }
        if (shopWit)
    }*/
}