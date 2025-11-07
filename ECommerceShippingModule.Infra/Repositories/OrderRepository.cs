using ECommerceShippingModule.Domain.Entities;
using ECommerceShippingModule.Domain.Interfaces.Repositories;
using ECommerceShippingModule.Infra.Context;

namespace ECommerceShippingModule.Infra.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DataContext context;

    public OrderRepository(DataContext context) 
    {
        this.context = context;
    }

    public void Add(Order entity)
    {
        context.Orders.Add(entity);
        context.SaveChanges();
    }

    public void Delete(Order entity)
    {
        context.Orders.Remove(entity);
        context.SaveChanges();
    }

    public Order GetById(Guid id)
    {
        return context.Orders.FirstOrDefault(o => o.Id == id);
    }

    public void Update(Order entity)
    {
        context.Orders.Update(entity);
        context.SaveChanges();
    }

    public IList<Order> GetAll()
    {
        return context.Orders.ToList();
    }
}
