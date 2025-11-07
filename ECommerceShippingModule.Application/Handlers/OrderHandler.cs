using ECommerceShippingModule.Application.Inputs;
using ECommerceShippingModule.Application.Intefaces.Handlers;
using ECommerceShippingModule.Application.Intefaces.Services;
using ECommerceShippingModule.Application.Outputs;
using ECommerceShippingModule.Domain.Entities;
using ECommerceShippingModule.Domain.Interfaces.Repositories;
using FluentValidation;

namespace ECommerceShippingModule.Application.Handlers;

public class OrderHandler : IOrderHandler
{
    private readonly IOrderRepository orderRepository;
    private readonly IValidator<OrderInput> orderValidator;
    private readonly IShippingCalculateFactory shippingCalculateFactory;
    public OrderHandler(IOrderRepository orderRepository,
                        IValidator<OrderInput> orderValidator,
                        IShippingCalculateFactory shippingCalculateFactory)
    {
        this.orderRepository = orderRepository;
        this.orderValidator = orderValidator;
        this.shippingCalculateFactory = shippingCalculateFactory;
    }

    public OrderOutput Create(OrderInput orderInput)
    {
        var resultValidator = orderValidator.Validate(orderInput);
        if (!resultValidator.IsValid)
        {
            throw new ValidationException(resultValidator.Errors);
        }

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

    public OrderOutput Update(OrderInput orderInput)
    {
        
    }
}
