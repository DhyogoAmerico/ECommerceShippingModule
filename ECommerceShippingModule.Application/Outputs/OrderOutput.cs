using ECommerceShippingModule.Domain.Entities;

namespace ECommerceShippingModule.Application.Outputs;

public class OrderOutput
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public decimal ShippingValue { get; set; }
    public string ShippingMode { get; set; }
    public decimal Weight { get; set; }
    public decimal Distance { get; set; }

    public static explicit operator OrderOutput(Order order)
    {
        return new OrderOutput
        {
            Id = order.Id,
            ClientId = order.ClientId,
            ShippingValue = order.ShippingValue,
            ShippingMode = order.ShippingMode.ToString(),
            Distance = order.Distance,
            Weight = order.Weight
        };
    }
}
