using Shops.Exceptions;

namespace Shops.Entities;

public class Person
{
    private readonly List<Order> _orders;

    public Person(Guid id, string firstName, decimal money)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentNullException(firstName);
        }

        if (money <= 0)
            throw new NotEnoughMoneyException("person doesn't have money");

        FirstName = firstName;
        Money = money;
        Id = id;
        _orders = new List<Order>();
    }

    public Guid Id { get; init; }
    public string FirstName { get; }
    public decimal Money { get; private set; }
    public IReadOnlyCollection<Order> Orders => _orders;

    public Order AddOrder(Guid orderId, int shopId)
    {
        var order = new Order(orderId, shopId, Id);
        _orders.Add(order);

        return order;
    }

    public Order AddItemsToOrder(Guid orderId, ItemInShop itemInShop)
    {
        Order order = _orders.Find(or => or.Id == orderId)
                      ?? throw new ShopManagerException("order's id doesn't contain in _orders");

        order.AddItem(itemInShop);

        return order;
    }

    public Order Buy(Guid orderId)
    {
        Order order = _orders.Find(ord => ord.Id == orderId)
                      ?? throw new ShopManagerException("order's id doesn't contain in _orders");
        if (order.IsPayed)
            throw new ShopManagerException("person can't pay order twice");
        if (Money - order.OrderAmount < 0)
            throw new NotEnoughMoneyException("not enough money to pay order");

        Money -= order.OrderAmount;
        order.Payed();

        return order;
    }
}