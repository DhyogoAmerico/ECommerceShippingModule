using ECommerceShippingModule.Domain.Entities;

namespace ECommerceShippingModule.Infra.DataContext;

public class Context
{
    public HashSet<Client> Clients { get; set; } = [];
    public HashSet<Product> Products { get; set; } = [];
    public HashSet<Order> Orders { get; set; } = [];
}
