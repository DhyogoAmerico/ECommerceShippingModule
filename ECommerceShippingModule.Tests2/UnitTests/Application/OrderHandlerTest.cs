using ECommerceShippingModule.Application.Handlers;
using ECommerceShippingModule.Application.Inputs;
using ECommerceShippingModule.Application.Intefaces.Services;
using ECommerceShippingModule.Domain.Entities;
using ECommerceShippingModule.Domain.Enums;
using ECommerceShippingModule.Domain.Interfaces.Repositories;
using FluentValidation;
using Moq;

namespace ECommerceShippingModule.Tests.UnitTests.Application;

[TestFixture]
public class OrderHandlerTest
{
    private Mock<IShippingCalculateFactory> shippingCalculateFactory;
    private Mock<IOrderRepository> orderRepository;

    [SetUp]
    public void Setup()
    {
        shippingCalculateFactory = new Mock<IShippingCalculateFactory>();
        orderRepository = new Mock<IOrderRepository>();
    }

    [Test]
    public void ShouldCreateOrderHandlerInstance()
    {
        var handler = new OrderHandler(orderRepository.Object, shippingCalculateFactory.Object);
        Assert.That(handler, Is.Not.Null);
    }

    [TestCaseSource(nameof(CasesCreateOrder))]
    public void Create_ShouldCalculateAndPersist_ForEachShippingMode(CaseOrder caseOrder)
    {
        // Arrange
        var calculator = new Mock<IShippingCalculateService>();
        calculator.Setup(c => c.Calculate(It.IsAny<decimal>(), It.IsAny<decimal>()))
            .Returns(caseOrder.ResultShipping);

        shippingCalculateFactory
            .Setup(f => f.GetShippingCalculateService(caseOrder.ShippingMode))
            .Returns(calculator.Object);

        var input = new CreateOrderInput
        {
            ClientId = Guid.NewGuid(),
            ShippingMode = caseOrder.ShippingMode,
            Weight = caseOrder.Weight,
            Distance = caseOrder.Distance
        };

        var handler = new OrderHandler(orderRepository.Object, shippingCalculateFactory.Object);

        // Act
        var output = handler.Create(input);

        // Assert
        Assert.That(output, Is.Not.Null);
        Assert.That(output.ClientId, Is.EqualTo(input.ClientId));
        Assert.That(output.ShippingMode, Is.EqualTo(caseOrder.ShippingMode.ToString()));
        Assert.That(output.ShippingValue, Is.EqualTo(caseOrder.ResultShipping));
        orderRepository.Verify(r => r.Add(It.Is<Order>(o =>
            o.ClientId == input.ClientId &&
            o.ShippingMode == caseOrder.ShippingMode &&
            o.Weight == input.Weight &&
            o.Distance == input.Distance &&
            o.ShippingValue == caseOrder.ResultShipping
        )), Times.Once);
    }

    [TestCaseSource(nameof(CasesCreateOrder))]
    public void Update_ShouldRecalculateAndPersist_ForEachShippingMode(CaseOrder caseOrder)
    {
        // Arrange existing order
        var existingOrder = new Order(Guid.NewGuid(), 10m, ShippingMode.Standard, 5m, 50m)
        {
            Id = Guid.NewGuid()
        };

        orderRepository.Setup(r => r.GetById(existingOrder.Id))
            .Returns(existingOrder);

        var calculator = new Mock<IShippingCalculateService>();
        calculator.Setup(c => c.Calculate(It.IsAny<decimal>(), It.IsAny<decimal>()))
            .Returns(caseOrder.ResultShipping);
        shippingCalculateFactory
            .Setup(f => f.GetShippingCalculateService(caseOrder.ShippingMode))
            .Returns(calculator.Object);

        var handler = new OrderHandler(orderRepository.Object, shippingCalculateFactory.Object);

        var input = new UpdateOrderInput
        {
            Id = existingOrder.Id,
            ClientId = existingOrder.ClientId,
            ShippingMode = caseOrder.ShippingMode,
            Weight = caseOrder.Weight,
            Distance = caseOrder.Distance
        };

        // Act
        var output = handler.Update(input);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output.Id, Is.EqualTo(existingOrder.Id));
            Assert.That(output.ClientId, Is.EqualTo(existingOrder.ClientId));
            Assert.That(output.ShippingMode, Is.EqualTo(caseOrder.ShippingMode.ToString()));
            Assert.That(output.ShippingValue, Is.EqualTo(caseOrder.ResultShipping));
        });

        orderRepository.Verify(r => r.Update(It.Is<Order>(o =>
            o.Id == existingOrder.Id &&
            o.ClientId == existingOrder.ClientId &&
            o.ShippingMode == caseOrder.ShippingMode &&
            o.Weight == caseOrder.Weight &&
            o.Distance == caseOrder.Distance &&
            o.ShippingValue == caseOrder.ResultShipping
        )), Times.Once);
    }

    [Test]
    public void GetById_ShouldReturnOrderOutput()
    {
        // Arrange
        var order = new Order(Guid.NewGuid(), 99m, ShippingMode.Express, 12m, 120m)
        {
            Id = Guid.NewGuid()
        };
        orderRepository.Setup(r => r.GetById(order.Id)).Returns(order);
        var handler = new OrderHandler(orderRepository.Object, shippingCalculateFactory.Object);

        // Act
        var output = handler.GetById(order.Id);

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(output.Id, Is.EqualTo(order.Id));
            Assert.That(output.ClientId, Is.EqualTo(order.ClientId));
            Assert.That(output.ShippingMode, Is.EqualTo(order.ShippingMode.ToString()));
            Assert.That(output.ShippingValue, Is.EqualTo(order.ShippingValue));
            Assert.That(output.Weight, Is.EqualTo(order.Weight));
            Assert.That(output.Distance, Is.EqualTo(order.Distance));
        });
    }

    [Test]
    public void GetOrders_ShouldReturnListOfOrderOutputs()
    {
        // Arrange
        var order1 = new Order(Guid.NewGuid(), 10m, ShippingMode.Standard, 1m, 10m) { Id = Guid.NewGuid() };
        var order2 = new Order(Guid.NewGuid(), 20m, ShippingMode.Scheduled, 2m, 20m) { Id = Guid.NewGuid() };

        orderRepository.Setup(r => r.GetAll()).Returns(new List<Order> { order1, order2 });

        var handler = new OrderHandler(orderRepository.Object, shippingCalculateFactory.Object);

        // Act
        var outputs = handler.GetOrders();

        // Assert
        Assert.That(outputs, Is.Not.Null);
        Assert.That(outputs, Has.Count.EqualTo(2));
        Assert.Multiple(() =>
        {
            Assert.That(outputs.Any(o => o.Id == order1.Id && o.ShippingMode == order1.ShippingMode.ToString()));
            Assert.That(outputs.Any(o => o.Id == order2.Id && o.ShippingMode == order2.ShippingMode.ToString()));
        });
    }

    [Test]
    public void Delete_ShouldDeleteOrder()
    {
        // Arrange
        var order = new Order(Guid.NewGuid(), 10m, ShippingMode.Standard, 1m, 10m) { Id = Guid.NewGuid() };
        orderRepository.Setup(r => r.GetById(order.Id)).Returns(order);
        var handler = new OrderHandler(orderRepository.Object, shippingCalculateFactory.Object);

        // Act
        handler.Delete(order.Id);

        // Assert
        orderRepository.Verify(r => r.Delete(order), Times.Once);
    }

    [TestCase(0, 10)]
    [TestCase(10, 0)]
    [TestCase(0, 0)]
    public void Create_ShouldThrowValidationException_WhenInvalidInput(decimal weight, decimal distance)
    {
        // Arrange
        var handler = new OrderHandler(orderRepository.Object, shippingCalculateFactory.Object);

        var input = new CreateOrderInput
        {
            ClientId = Guid.NewGuid(),
            ShippingMode = ShippingMode.Standard,
            Weight = weight,
            Distance = distance
        };

        // Act & Assert
        Assert.Throws<ValidationException>(() => handler.Create(input));
        orderRepository.Verify(r => r.Add(It.IsAny<Order>()), Times.Never);
    }

    [Test]
    public void Update_ShouldThrowKeyNotFound_WhenOrderNotFound()
    {
        // Arrange
        orderRepository.Setup(r => r.GetById(It.IsAny<Guid>()))
            .Returns(null as Order);

        var handler = new OrderHandler(orderRepository.Object, shippingCalculateFactory.Object);

        var input = new UpdateOrderInput
        {
            Id = Guid.NewGuid(),
            ClientId = Guid.NewGuid(),
            ShippingMode = ShippingMode.Standard,
            Weight = 1,
            Distance = 1
        };

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => handler.Update(input));
    }

    [Test]
    public void Update_ShouldThrowValidationException_WhenInvalidInput()
    {
        // Arrange: ensure order exists to trigger validator
        var existing = new Order(Guid.NewGuid(), 10m, ShippingMode.Standard, 5m, 5m) { Id = Guid.NewGuid() };
        orderRepository.Setup(r => r.GetById(existing.Id)).Returns(existing);

        var handler = new OrderHandler(orderRepository.Object, shippingCalculateFactory.Object);

        var input = new UpdateOrderInput
        {
            Id = existing.Id,
            ClientId = existing.ClientId,
            ShippingMode = ShippingMode.Express,
            Weight = 0, // invalid
            Distance = 10
        };

        // Act & Assert
        Assert.Throws<ValidationException>(() => handler.Update(input));
        orderRepository.Verify(r => r.Update(It.IsAny<Order>()), Times.Never);
    }

    public static IEnumerable<TestCaseData> CasesCreateOrder
    {
        get
        {
            yield return new TestCaseData(new CaseOrder(ShippingMode.Standard, 5m, 50m, 17.5m));
            yield return new TestCaseData(new CaseOrder(ShippingMode.Express, 10m, 100m, 60m));
            yield return new TestCaseData(new CaseOrder(ShippingMode.Scheduled, 20m, 200m, 70m));
        }
    }
}

public record CaseOrder(
    ShippingMode ShippingMode,
    decimal Weight,
    decimal Distance,
    decimal ResultShipping
);