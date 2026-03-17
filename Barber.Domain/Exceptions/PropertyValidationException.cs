namespace Barber.Domain.Exceptions;

public class PropertyValidationException : Exception
{
    public Dictionary<string, string[]>? Errors { get; set; }

    public PropertyValidationException(Dictionary<string, string[]> errors)
    {
        Errors = errors;
    }
}
