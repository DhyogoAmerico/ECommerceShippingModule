namespace ECommerceShippingModule.Application.Intefaces.Services;

public interface IShippingCalculateService
{
    decimal Calculate(decimal weight, decimal distance);
}
