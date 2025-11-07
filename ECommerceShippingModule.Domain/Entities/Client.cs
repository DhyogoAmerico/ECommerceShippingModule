namespace ECommerceShippingModule.Domain.Entities;

public class Client : EntityBase
{
    public string Name { get; set; }
    public string Address { get; set; }
    private Client() { }
    public Client(string name, string address)
    {
        Name = name;
        Address = address;
    }
}
