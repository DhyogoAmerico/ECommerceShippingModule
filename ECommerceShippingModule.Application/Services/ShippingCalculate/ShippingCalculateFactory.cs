using ECommerceShippingModule.Application.Intefaces.Services;
using ECommerceShippingModule.Domain.Enums;

namespace ECommerceShippingModule.Application.Services.ShippingCalculate;

public class ShippingCalculateFactory : IShippingCalculateFactory
{
    public IShippingCalculateService GetShippingCalculateService(ShippingMode shippingMode)
    {
        return shippingMode switch
        {
            ShippingMode.Standard => new StandardCalculate(),
            ShippingMode.Express => new ExpressCalculate(),
            ShippingMode.Scheduled => new ScheduledCalculate(),
            _ => throw new NotImplementedException($"Shipping calculation for mode {shippingMode} is not implemented."),
        };
    }
}
