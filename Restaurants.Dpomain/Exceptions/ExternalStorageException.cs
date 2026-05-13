namespace Restaurants.Domain.Exceptions;

public class ExternalStorageException(string message, Exception? innerException = null)
    : Exception(message, innerException)
{
}
