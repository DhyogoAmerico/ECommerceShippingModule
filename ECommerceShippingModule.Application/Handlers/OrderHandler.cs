using ECommerceShippingModule.Application.Inputs;
using ECommerceShippingModule.Application.Intefaces.Handlers;
using ECommerceShippingModule.Application.Intefaces.Services;
using ECommerceShippingModule.Application.Outputs;
using ECommerceShippingModule.Application.Validators;
using ECommerceShippingModule.Domain.Entities;
using ECommerceShippingModule.Domain.Interfaces.Repositories;
using FluentValidation;

namespace ECommerceShippingModule.Application.Handlers;

public class OrderHandler : BaseHandler, IOrderHandler
{
    private readonly IOrderRepository orderRepository;
    private readonly IShippingCalculateFactory shippingCalculateFactory;
    public OrderHandler(IOrderRepository orderRepository,
                        IShippingCalculateFactory shippingCalculateFactory)
    {
        this.orderRepository = orderRepository;
        this.shippingCalculateFactory = shippingCalculateFactory;
    }

    public OrderOutput Create(CreateOrderInput orderInput)
    {
        Validate(new CreateOrderValidator(), orderInput);

        var calculator = shippingCalculateFactory.GetShippingCalculateService(orderInput.ShippingMode);
        var shippingValue = calculator.Calculate(orderInput.Weight, orderInput.Distance);

        var order = new Order(orderInput.ClientId, shippingValue, orderInput.ShippingMode, orderInput.Weight, orderInput.Distance);

        orderRepository.Add(order);

        return (OrderOutput)order;
    }

    public void Delete(Guid orderId)
    {
        var result = orderRepository.GetById(orderId);
        orderRepository.Delete(result);
    }

    public OrderOutput GetById(Guid orderId)
    {
        return (OrderOutput)orderRepository.GetById(orderId);
    }

    public IList<OrderOutput> GetOrders()
    {
        return orderRepository.GetAll().Select(order => (OrderOutput)order).ToList();
    }

    public OrderOutput Update(UpdateOrderInput orderInput)
    {
        var order = orderRepository.GetById(orderInput.Id);

        if (order is null)
            throw new KeyNotFoundException("Order not found.");

        Validate(new UpdateOrderValidator(), orderInput);

        var calculator = shippingCalculateFactory.GetShippingCalculateService(orderInput.ShippingMode);
        var shippingValue = calculator.Calculate(orderInput.Weight, orderInput.Distance);

        order.Distance = orderInput.Distance;
        order.Weight = orderInput.Weight;
        order.ShippingMode = orderInput.ShippingMode;
        order.ShippingValue = shippingValue;

        orderRepository.Update(order);

        return (OrderOutput)order;
    }
}
