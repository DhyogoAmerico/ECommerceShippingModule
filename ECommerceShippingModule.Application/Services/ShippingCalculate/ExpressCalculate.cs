using ECommerceShippingModule.Application.Intefaces.Services;

namespace ECommerceShippingModule.Application.Services.ShippingCalculate;

public class ExpressCalculate : IShippingCalculateService
{
    private const decimal BaseRate = 10.00m;
    public decimal Calculate(decimal weight, decimal distance)
    {
        return BaseRate + (weight * 1.0m) + (distance * 0.5m);
    }
}
