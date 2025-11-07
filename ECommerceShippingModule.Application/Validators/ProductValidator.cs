using ECommerceShippingModule.Application.Inputs;
using FluentValidation;

namespace ECommerceShippingModule.Application.Validators;

public class ProductValidator : AbstractValidator<ProductInput>
{
    public ProductValidator()
    {
        RuleFor(product => product.Name)
            .NotEmpty()
            .WithMessage("Product name is required.")
            .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");
        RuleFor(product => product.Weight)
            .GreaterThan(0)
            .WithMessage("Product weight must be greater than 0.");
    }
}
