using ECommerceShippingModule.Domain.Enums;

namespace ECommerceShippingModule.Application.Inputs;

public class UpdateOrderInput
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public ShippingMode ShippingMode { get; set; }
    public decimal Weight { get; set; }
    public decimal Distance { get; set; }
}
