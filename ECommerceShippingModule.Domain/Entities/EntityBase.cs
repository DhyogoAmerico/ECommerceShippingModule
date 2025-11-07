namespace ECommerceShippingModule.Domain.Entities;

public class EntityBase
{
    public Guid Id { get; set; }

    protected EntityBase()
    {
        Id = Guid.NewGuid();
    }
}
