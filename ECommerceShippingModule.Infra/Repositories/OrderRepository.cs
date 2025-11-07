using ECommerceShippingModule.Domain.Entities;
using ECommerceShippingModule.Domain.Interfaces.Repositories;
using ECommerceShippingModule.Infra.DataContext;

namespace ECommerceShippingModule.Infra.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(Context context)
    {
        Entities = context.Orders;
    }
}
