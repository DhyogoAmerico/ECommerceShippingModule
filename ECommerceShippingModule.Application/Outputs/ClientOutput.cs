using ECommerceShippingModule.Domain.Entities;

namespace ECommerceShippingModule.Application.Outputs;

public class ClientOutput
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }

    public static explicit operator ClientOutput(Client client)
    {
        return new ClientOutput
        {
            Id = client.Id,
            Name = client.Name,
            Address = client.Address
        };
    }
}
