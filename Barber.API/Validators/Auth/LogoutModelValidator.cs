using Barber.API.Models.Auth;
using FluentValidation;

namespace Barber.API.Validators.Auth;

public class LogoutModelValidator : AbstractValidator<LogoutModel>
{
    public LogoutModelValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("RefreshToken é obrigatório.");
    }
}
