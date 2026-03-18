using FluentValidation.Results;

namespace Barber.Domain.Exceptions;

public class InputValidationException : Exception
{
    public Dictionary<string, string[]>? Errors { get; set; }

    public InputValidationException(ValidationResult result)
    {
        Errors = result.Errors
            .GroupBy(err => err.PropertyName)
            .ToDictionary(
                group => group.Key,
                group => group.Select(e => e.ErrorMessage).ToArray()
            );
    }
}
