using Shops.Exceptions;

namespace Shops.Entities;

public class Shop
{
    private static int _id = 100000;
    private readonly List<ItemInShop> _items;

    public Shop(string address, string shopName, List<ItemInShop> items)
    {
        ArgumentNullException.ThrowIfNull(items);
        if (string.IsNullOrWhiteSpace(address))
        {
            throw new ArgumentNullException("address");
        }

        if (string.IsNullOrWhiteSpace(shopName))
        {
            throw new ArgumentNullException("shopName");
        }

        Address = address;
        ShopName = shopName;
        _items = items;
        Id = _id++;
    }

    public string Address { get; }
    public string ShopName { get; }
    public int Id { get; }
    public List<ItemInShop> Items => _items;

    public void BuyItem(Person person, List<ItemInShop> items)
    {
        ArgumentNullException.ThrowIfNull(person);
        ArgumentNullException.ThrowIfNull(items);

        decimal totalSum = 0;
        foreach (ItemInShop item in items)
        {
            var currentItem = _items.Find(itemInShop => itemInShop.Equals(item));
            if (currentItem == null)
            {
                throw new ShopManagerException("item doesn't exist in shop");
            }

            if (currentItem.Amount < item.Amount)
            {
                throw new ShopManagerException("isn't enough item in shop");
            }

            totalSum += currentItem.Cost * currentItem.Amount;
        }

        person.Buy(totalSum);

        foreach (ItemInShop item in items)
        {
            var currentItem = items.Find(itemInShop => itemInShop.Equals(item));
            ArgumentNullException.ThrowIfNull(currentItem);
            currentItem.ChangeAmount(currentItem.Amount - item.Amount);
        }
    }

    public void AddItem(List<ItemInShop> items)
    {
        ArgumentNullException.ThrowIfNull(items);
        foreach (ItemInShop item in items)
        {
            var currentItem = _items.Find(itemInShop => itemInShop.Equals(item));
            if (currentItem == null)
            {
                _items.Add(item);
            }
            else
            {
                var newAmount = currentItem.Amount + 1;
                currentItem.ChangeAmount(newAmount);
            }
        }
    }

    public void ChangeCost(decimal newCost, ItemInShop item)
    {
        ArgumentNullException.ThrowIfNull(item);
        var currentItem = _items.Find(itemInShop => itemInShop.Equals(item));
        if (currentItem == null)
        {
            throw new ShopManagerException("item doesn't exist in shop");
        }

        currentItem.ChangeCost(newCost);
    }
}