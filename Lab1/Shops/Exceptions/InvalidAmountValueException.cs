namespace Shops.Exceptions;

public class InvalidAmountValueException : Exception
{
    public InvalidAmountValueException(string message)
        : base(message) { }
}