using ECommerceShippingModule.Application.Intefaces.Services;

namespace ECommerceShippingModule.Application.Services.ShippingCalculate;

public class ScheduledCalculate : IShippingCalculateService
{
    private const decimal BaseRate = 7.50m;
    public decimal Calculate(decimal weight, decimal distance)
    {
        return BaseRate + (weight * 0.7m) + (distance * 0.3m);
    }
}
