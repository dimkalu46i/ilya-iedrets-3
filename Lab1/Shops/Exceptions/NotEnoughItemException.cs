namespace Shops.Exceptions;

public class NotEnoughItemException : Exception
{
    public NotEnoughItemException(string message)
        : base(message) { }
}