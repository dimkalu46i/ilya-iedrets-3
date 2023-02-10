using Shops.Entities;
using Shops.Exceptions;

namespace Shops.Services;

public class ShopManager : IShopManager
{
    private static int _id = 100000;
    private readonly List<Shop> _shops = new ();
    private readonly List<Person> _people = new ();

    public Shop AddShop(string address, string shopName)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentNullException(address);
        if (string.IsNullOrWhiteSpace(shopName))
            throw new ArgumentNullException(shopName);
        if (_shops.Any(shop => shop.ShopName.Equals(shopName)
                               && shop.Address.Equals(address)))
        {
            throw new ShopManagerException("shop already contains in _shops");
        }

        var currentShop = new Shop(++_id, address, shopName, new List<ItemInShop>());
        _shops.Add(currentShop);

        return currentShop;
    }

    public ItemInShop AddItemToShop(Shop shop, ItemName itemName, decimal cost, int amount = 1)
    {
        ArgumentNullException.ThrowIfNull(shop);
        ArgumentNullException.ThrowIfNull(itemName);
        if (cost < 0)
            throw new InvalidCostValueException("cost value have to be positive");

        var itemInShop = new ItemInShop(itemName, cost, amount);
        shop.AddItem(itemInShop);

        return itemInShop;
    }

    public Person AddPerson(string firstName, decimal money)
    {
        var person = new Person(Guid.NewGuid(), firstName, money);
        _people.Add(person);

        return person;
    }

    public Order AddOrderToPerson(Shop shop, Person person)
    {
        ArgumentNullException.ThrowIfNull(shop);
        ArgumentNullException.ThrowIfNull(person);

        return person.AddOrder(Guid.NewGuid(), shop.Id);
    }

    public Order AddItemsToOrder(Shop shop, Person person, Guid orderId, ItemInShop itemInShop)
    {
        ArgumentNullException.ThrowIfNull(shop);
        ArgumentNullException.ThrowIfNull(person);

        shop.SellItem(itemInShop);

        return person.AddItemsToOrder(orderId, itemInShop);
    }

    public Order PayOrder(Person person, Guid orderId)
    {
        ArgumentNullException.ThrowIfNull(person);

        return person.Buy(orderId);
    }

    public Shop? GetShopWithBestOffer(ItemName itemName, int amount = 1)
    {
        ArgumentNullException.ThrowIfNull(itemName);

// from shop in _shops let itemInShop = shop.Items.Find(item => item.ItemName.Equals(itemName)) where itemInShop is not null where itemInShop.Amount >= amount && itemInShop.Cost < minPrice select shop)
        Shop? foundShop = null;
        decimal minPrice = decimal.MaxValue;
        foreach (Shop shop in _shops)
        {
            ItemInShop? itemInShop = shop.Items.Find(item => item.ItemName.Name == itemName.Name);
            if (itemInShop != null && itemInShop.Amount >= amount && itemInShop.Cost < minPrice)
            {
                minPrice = itemInShop.Cost;
                foundShop = shop;
            }
        }

        return foundShop;
    }
}