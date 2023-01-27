using Shops.Exceptions;

namespace Shops.Entities;

public class ItemInShop
{
    public ItemInShop(ItemName itemName, int amount, decimal cost = 0)
    {
        ArgumentNullException.ThrowIfNull(itemName);
        if (cost < 0 || amount <= 0)
        {
            throw new ShopManagerException("cost or amount has ");
        }

        ItemName = itemName;
        Cost = cost;
        Amount = amount;
    }

    public decimal Cost { get; private set; }
    public int Amount { get; private set; }
    public ItemName ItemName { get; }

    public void ChangeAmount(int newAmount)
    {
        if (newAmount <= 0)
        {
            throw new ShopManagerException("cost or amount has ");
        }

        Amount = newAmount;
    }

    public void ChangeCost(decimal newCost)
    {
        if (newCost <= 0)
        {
            throw new ShopManagerException("cost or amount has ");
        }

        Cost = newCost;
    }
}