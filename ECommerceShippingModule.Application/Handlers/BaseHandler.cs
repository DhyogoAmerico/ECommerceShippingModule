using FluentValidation;

namespace ECommerceShippingModule.Application.Handlers;

public abstract class BaseHandler
{
    protected void Validate<T>(IValidator<T> validator, T input)
    {
        var result = validator.Validate(input);
        if (result.IsValid) return;

        throw new ValidationException(result.Errors);
    }
}
