namespace Barber.Domain.Exceptions;

public class InputValidationException : Exception
{
    public Dictionary<string, string[]>? Errors { get; set; }

    public InputValidationException(Dictionary<string, string[]> errors)
    {
        Errors = errors;
    }
}
