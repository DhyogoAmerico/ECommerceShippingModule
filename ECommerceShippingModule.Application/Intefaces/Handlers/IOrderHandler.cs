using ECommerceShippingModule.Application.Inputs;
using ECommerceShippingModule.Application.Outputs;

namespace ECommerceShippingModule.Application.Intefaces.Handlers;

public interface IOrderHandler
{
    OrderOutput Create(OrderInput orderInput);
    OrderOutput Update(OrderInput orderInput);
    void Delete(Guid orderId);
    OrderOutput GetById(Guid orderId);
    IList<OrderOutput> GetOrders();
}
