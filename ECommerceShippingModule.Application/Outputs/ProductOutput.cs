using ECommerceShippingModule.Domain.Entities;

namespace ECommerceShippingModule.Application.Outputs;

public class ProductOutput
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Weight { get; set; }

    public static explicit operator ProductOutput(Product product)
    {
        return new ProductOutput
        {
            Id = product.Id,
            Name = product.Name,
            Weight = product.Weight
        };
    }
}
