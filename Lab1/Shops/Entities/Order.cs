using Shops.Exceptions;

namespace Shops.Entities;

public class Order
{
    private readonly List<ItemInShop> _items;

    public Order(Guid id, int shopId, Guid personId)
    {
        Id = id;
        ShopId = shopId;
        PersonId = personId;
        _items = new List<ItemInShop>();
        IsPayed = false;
        OrderAmount = 0;
    }

    public Guid Id { get; init; }
    public int ShopId { get; init; }
    public Guid PersonId { get; init; }
    public decimal OrderAmount { get; private set; }
    public bool IsPayed { get; private set; }
    public IReadOnlyCollection<ItemInShop> ItemInShops => _items;

    public ItemInShop AddItem(ItemInShop itemInShop)
    {
        if (_items.Contains(itemInShop))
            throw new ShopManagerException("item already contains in order");
        ArgumentNullException.ThrowIfNull(itemInShop);

        OrderAmount += itemInShop.Cost * itemInShop.Amount;
        _items.Add(itemInShop);

        return itemInShop;
    }

    public void Payed()
    {
        IsPayed = true;
    }
}