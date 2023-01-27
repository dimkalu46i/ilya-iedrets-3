namespace Shops.Entities;

public class ItemName
{
    public ItemName(string itemName)
    {
        if (string.IsNullOrWhiteSpace(itemName))
        {
            throw new ArgumentNullException(itemName);
        }

        Name = itemName;
    }

    public string Name { get; }
}