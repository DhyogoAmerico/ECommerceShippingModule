using ECommerceShippingModule.Domain.Enums;

namespace ECommerceShippingModule.Application.Intefaces.Services;

public interface IShippingCalculateFactory
{
    IShippingCalculateService GetShippingCalculateService(ShippingMode shippingMode);
}