using Shops.Exceptions;

namespace Shops.Entities;

public class Shop
{
    private readonly List<ItemInShop> _items;

    public Shop(int id, string address, string shopName, List<ItemInShop> items)
    {
        ArgumentNullException.ThrowIfNull(items);
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentNullException(address);

        if (string.IsNullOrWhiteSpace(shopName))
            throw new ArgumentNullException(shopName);

        Address = address;
        ShopName = shopName;
        _items = items;
        Id = id;
    }

    public string Address { get; }
    public string ShopName { get; }
    public int Id { get; }
    public List<ItemInShop> Items => _items;

    public void SellItem(ItemInShop item)
    {
        ArgumentNullException.ThrowIfNull(item);

        ItemInShop itemInShop = _items.Find(i => i.ItemName == item.ItemName)
                                ?? throw new NotEnoughItemException("item doesn't contain is _items");
        if (itemInShop.Amount - item.Amount < 0)
            throw new InvalidAmountValueException("not enough item to sell");

        itemInShop.ChangeAmount(item.Amount);
    }

    public ItemInShop AddItem(ItemInShop item)
    {
        ArgumentNullException.ThrowIfNull(item);

        ItemInShop? currentItem = _items.Find(itemInShop => itemInShop.Equals(item));
        if (currentItem == null)
        {
            _items.Add(item);
        }
        else
        {
            int newAmount = currentItem.Amount + 1;
            currentItem.ChangeAmount(newAmount);
        }

        return item;
    }

    public void ChangeCost(decimal newCost, ItemInShop item)
    {
        ArgumentNullException.ThrowIfNull(item);
        ItemInShop? currentItem = _items.Find(itemInShop => itemInShop.Equals(item)
                                                    && itemInShop.Amount >= item.Amount);
        if (currentItem == null)
        {
            throw new ShopManagerException("item doesn't contain in shop");
        }

        currentItem.ChangeCost(newCost);
    }
}