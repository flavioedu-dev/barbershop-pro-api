using Barber.API.Models.Auth;
using FluentValidation;

namespace Barber.API.Validators.Auth;

public class RevalidateTokenModelValidator : AbstractValidator<RevalidateTokenModel>
{
    public RevalidateTokenModelValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("RefreshToken é obrigatório.");
    }
}
