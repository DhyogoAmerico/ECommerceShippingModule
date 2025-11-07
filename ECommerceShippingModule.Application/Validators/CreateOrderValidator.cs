using ECommerceShippingModule.Application.Inputs;
using FluentValidation;

namespace ECommerceShippingModule.Application.Validators;

public class CreateOrderValidator : AbstractValidator<CreateOrderInput>
{
    public CreateOrderValidator()
    {
        RuleFor(order => order.ClientId)
            .NotNull()
            .WithMessage("ClientId is required.");
        RuleFor(order => order.ShippingMode)
            .IsInEnum()
            .WithMessage("ShippingMode must be a valid enum value.");
        RuleFor(order => order.Weight)
            .GreaterThan(0)
            .WithMessage("Weight must be greater than zero.");
        RuleFor(order => order.Distance)
            .GreaterThan(0)
            .WithMessage("Distance must be greater than zero.");
    }
}
