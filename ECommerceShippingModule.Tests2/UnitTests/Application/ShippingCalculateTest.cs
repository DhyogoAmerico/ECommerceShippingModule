using ECommerceShippingModule.Application.Intefaces.Services;
using ECommerceShippingModule.Application.Services.ShippingCalculate;

namespace ECommerceShippingModule.Tests.UnitTests.Application;

[TestFixture]
public class ShippingCalculateTest
{
    [TestCaseSource(nameof(CasesCalculateShipping))]
    public void ShouldCalculateShippingCostCorrectly(CaseShippingCalculate shippingCalculate)
    {
        //Arrange
        var service = shippingCalculate.ShippingCalculateService;

        //Act
        var resultCost = service.Calculate(shippingCalculate.Weight, shippingCalculate.Distance);

        //Assert
        Assert.That(resultCost, Is.EqualTo(shippingCalculate.ExpectedCost));
    }

    public static IEnumerable<TestCaseData> CasesCalculateShipping
    {
        get
        {
            yield return new TestCaseData(new CaseShippingCalculate(
                new StandardCalculate(), 5m, 50m, 17.5m
            ));
            yield return new TestCaseData(new CaseShippingCalculate(
                new ExpressCalculate(), 10m, 100m, 70m
            ));
            yield return new TestCaseData(new CaseShippingCalculate(
                new ScheduledCalculate(), 20m, 200m, 81.5m
            ));
        }
    }
}

public record CaseShippingCalculate(
    IShippingCalculateService ShippingCalculateService,
    decimal Weight,
    decimal Distance,
    decimal ExpectedCost
);