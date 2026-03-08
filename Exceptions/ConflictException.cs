namespace DeliveryApi.Exceptions;

public class ConflictExcption : Exception
{
    public ConflictExcption(string message) : base(message) { }
}