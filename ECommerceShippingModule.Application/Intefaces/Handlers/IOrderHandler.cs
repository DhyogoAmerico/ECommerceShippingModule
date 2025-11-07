using ECommerceShippingModule.Application.Inputs;
using ECommerceShippingModule.Application.Outputs;

namespace ECommerceShippingModule.Application.Intefaces.Handlers;

public interface IOrderHandler
{
    OrderOutput Create(CreateOrderInput orderInput);
    OrderOutput Update(UpdateOrderInput orderInput);
    void Delete(Guid orderId);
    OrderOutput GetById(Guid orderId);
    IList<OrderOutput> GetOrders();
}
