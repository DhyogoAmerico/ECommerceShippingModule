using ECommerceShippingModule.Application.Intefaces.Services;

namespace ECommerceShippingModule.Application.Services.ShippingCalculate;

public class StandardCalculate : IShippingCalculateService
{
    private const decimal BaseRate = 5.00m;
    public decimal Calculate(decimal weight, decimal distance)
    {
        return BaseRate + (weight * 0.5m) + (distance * 0.2m);
    }
}
