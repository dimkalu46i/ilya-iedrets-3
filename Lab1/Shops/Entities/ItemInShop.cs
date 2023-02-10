using Shops.Exceptions;

namespace Shops.Entities;

public class ItemInShop
{
    public ItemInShop(ItemName itemName, decimal cost, int amount = 1)
    {
        ArgumentNullException.ThrowIfNull(itemName);
        if (cost < 0)
            throw new InvalidCostValueException("cost value have to be positive");

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
            throw new InvalidAmountValueException("amount value have to be positive");

        Amount = newAmount;
    }

    public void ChangeCost(decimal newCost)
    {
        if (newCost <= 0)
            throw new InvalidCostValueException("cost value have to be positive");

        Cost = newCost;
    }
}