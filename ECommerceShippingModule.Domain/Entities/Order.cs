using ECommerceShippingModule.Domain.Enums;

namespace ECommerceShippingModule.Domain.Entities;

public class Order : EntityBase
{
    public Guid ClientId { get; set; }
    public decimal ShippingValue { get; set; }
    public ShippingMode ShippingMode { get; set; }
    public decimal Weight { get; set; }
    public decimal Distance { get; set; }

    private Order() { }

    public Order(Guid clientId, decimal shippingValue, ShippingMode shippingMode, decimal weight, decimal distance)
    {
        ClientId = clientId;
        ShippingValue = shippingValue;
        ShippingMode = shippingMode;
        Weight = weight;
        Distance = distance;
    }
}
