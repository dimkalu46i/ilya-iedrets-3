using Shops.Entities;

namespace Shops.Services;

public interface IShopManager
{
    Shop AddShop(string address, string shopName);
    ItemInShop AddItemToShop(Shop shop, ItemName itemName, decimal cost, int amount = 1);
    Person AddPerson(string firstName, decimal money);
    Order AddOrderToPerson(Shop shop, Person person);
    Order PayOrder(Person person, Guid orderId);
    Order AddItemsToOrder(Shop shop, Person person, Guid orderId, ItemInShop itemInShop);
    Shop? GetShopWithBestOffer(ItemName itemName, int amount = 1);
}