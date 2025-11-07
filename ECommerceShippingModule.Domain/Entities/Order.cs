using ECommerceShippingModule.Domain.Enums;

namespace ECommerceShippingModule.Domain.Entities;

public class Order : EntityBase
{
    public Guid ClientId { get; set; }
    public double ShippingValue { get; set; }
    public ShippingMode ShippingMode { get; set; }

    private Order() { }

    public Order(Guid clientId, double shippingValue, ShippingMode shippingMode)
    {
        ClientId = clientId;
        ShippingValue = shippingValue;
        ShippingMode = shippingMode;
    }
}
