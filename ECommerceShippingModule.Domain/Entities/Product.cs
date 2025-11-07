namespace ECommerceShippingModule.Domain.Entities;

public class Product : EntityBase
{
    public string Name { get; set; }
    public decimal Weight { get; set; }
    private Product() { }
    public Product(string name, double weight)
    {
        Name = name;
        Weight = weight;
    }
}
