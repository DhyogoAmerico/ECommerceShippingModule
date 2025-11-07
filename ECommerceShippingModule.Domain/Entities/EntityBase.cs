using ECommerceShippingModule.Domain.Interfaces.Entities;

namespace ECommerceShippingModule.Domain.Entities;

public class EntityBase : IEntity
{
    public Guid Id { get; set; }

    protected EntityBase()
    {
        Id = Guid.NewGuid();
    }
}
