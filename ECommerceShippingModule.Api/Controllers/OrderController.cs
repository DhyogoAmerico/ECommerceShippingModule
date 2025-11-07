using ECommerceShippingModule.Application.Inputs;
using ECommerceShippingModule.Application.Intefaces.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceShippingModule.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderHandler orderHandler;
    public OrderController(IOrderHandler orderHandler)
    {
        this.orderHandler = orderHandler;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateOrderInput orderInput)
    {
        var result = orderHandler.Create(orderInput);
        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update([FromBody] UpdateOrderInput orderInput)
    {
        var result = orderHandler.Update(orderInput);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        orderHandler.Delete(id);
        return NoContent();
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var result = orderHandler.GetById(id);
        return Ok(result);
    }

    [HttpGet]
    public IActionResult GetOrders()
    {
        var result = orderHandler.GetOrders();
        return Ok(result);
    }
}
