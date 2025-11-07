using ECommerceShippingModule.Domain.Entities;

namespace ECommerceShippingModule.Application.Outputs;

public class OrderOutput
{
    public Guid Id { get; set; }
    public ClientOutput Client { get; set; }
    public decimal ShippingValue { get; set; }
    public string ShippingMode { get; set; }
    public ProductOutput Product { get; set; }

    public static explicit operator OrderOutput(Order order)
    {
        return new OrderOutput
        {
            Id = order.Id,
            Client = (ClientOutput)order.Client,
            ShippingValue = order.ShippingValue,
            ShippingMode = order.ShippingMode.ToString(),
            Product = (ProductOutput)order.Product
        };
    }
}
