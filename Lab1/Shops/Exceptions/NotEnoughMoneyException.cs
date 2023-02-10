namespace Shops.Exceptions;

public class NotEnoughMoneyException : Exception
{
    public NotEnoughMoneyException(string message)
        : base(message) { }
}