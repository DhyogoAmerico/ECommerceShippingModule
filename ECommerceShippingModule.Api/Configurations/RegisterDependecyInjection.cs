using ECommerceShippingModule.Application.Handlers;
using ECommerceShippingModule.Application.Inputs;
using ECommerceShippingModule.Application.Intefaces.Handlers;
using ECommerceShippingModule.Application.Intefaces.Services;
using ECommerceShippingModule.Application.Services.ShippingCalculate;
using ECommerceShippingModule.Application.Validators;
using ECommerceShippingModule.Domain.Interfaces.Repositories;
using ECommerceShippingModule.Infra.Context;
using ECommerceShippingModule.Infra.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ECommerceShippingModule.Api.Configurations;

public static class RegisterDependecyInjection
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddDbContext<DataContext>(opt => 
            opt.UseInMemoryDatabase("ECommerceShippingDb"));

        services.AddScoped<IOrderRepository, OrderRepository>();
    }

    public static void AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IOrderHandler, OrderHandler>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IShippingCalculateFactory, ShippingCalculateFactory>();
    }

    public static void AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateOrderInput>, CreateOrderValidator>();
    }
}
