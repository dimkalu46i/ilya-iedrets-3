using Shops.Exceptions;

namespace Shops.Entities;

public class Person
{
    public Person(string firstName, decimal money)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentNullException("firstName");
        }

        if (money <= 0)
        {
            throw new ShopManagerException("person doesn't have money");
        }

        FirstName = firstName;
        Money = money;
    }

    public string FirstName { get; }
    public decimal Money { get; private set; }

    public void Buy(decimal money)
    {
        if (money <= 0)
        {
            throw new ShopManagerException("money shoul be >0");
        }

        if (money > Money)
        {
            throw new ShopManagerException("person doesn't have enough money");
        }

        Money -= money;
    }
}