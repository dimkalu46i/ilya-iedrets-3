namespace Shops.Exceptions;

public class InvalidCostValueException : Exception
{
    public InvalidCostValueException(string message)
        : base(message) { }
}