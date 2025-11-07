using ECommerceShippingModule.Application.Inputs;
using FluentValidation;

namespace ECommerceShippingModule.Application.Validators;

public class ClientValidator : AbstractValidator<ClientInput>
{
    public ClientValidator()
    {
        RuleFor(client => client.Name)
            .NotEmpty()
            .WithMessage("Client name is required.")
            .MaximumLength(100).WithMessage("Client name must not exceed 100 characters.");
        RuleFor(client => client.Address)
            .NotEmpty()
            .WithMessage("Client address is required.")
            .MaximumLength(200).WithMessage("Client address must not exceed 200 characters.");
    }
}
