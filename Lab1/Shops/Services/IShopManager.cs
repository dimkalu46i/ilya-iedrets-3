using Shops.Entities;

namespace Shops.Services;

public interface IShopManager
{
    Shop AddShop(string address, string shopName);
    ItemInShop AddItem(ItemName itemName, int amount, decimal cost);
    Person AddPerson(string firstName, decimal money);

    // Shop GetShopWithBestOffer(ItemInShop item);
}