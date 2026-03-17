using Barber.API.Models.Auth;
using FluentValidation;

namespace Barber.API.Validations.Auth;

public class RegisterUserAndBarbershopModelValidator : AbstractValidator<RegisterUserAndBarbershopModel>
{
    public RegisterUserAndBarbershopModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome é obrigatório.")
            .MinimumLength(3).WithMessage("Nome deve ter no mínimo 3 caracteres.")
            .MaximumLength(120).WithMessage("Nome deve ter no máximo 120 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório.")
            .EmailAddress().WithMessage("Email inválido.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Senha é obrigatória.")
            .MinimumLength(8).WithMessage("Senha deve ter no mínimo 8 caracteres.")
            .MaximumLength(100).WithMessage("Senha deve ter no máximo 100 caracteres.");

        RuleFor(x => x.BarbershopName)
            .NotEmpty().WithMessage("Nome da barbearia é obrigatório.")
            .MinimumLength(2).WithMessage("Nome da barbearia deve ter no mínimo 2 caracteres.")
            .MaximumLength(120).WithMessage("Nome da barbearia deve ter no máximo 120 caracteres.");

        RuleFor(x => x.BarbershopPhoneNumber)
            .NotEmpty().WithMessage("Telefone da barbearia é obrigatório.")
            .Matches(@"^\d{10,11}$").WithMessage("Telefone da barbearia deve conter 10 ou 11 dígitos (somente números).");
    }
}

