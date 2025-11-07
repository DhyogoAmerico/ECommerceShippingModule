using ECommerceShippingModule.Application.Inputs;
using ECommerceShippingModule.Application.Outputs;

namespace ECommerceShippingModule.Application.Intefaces.Handlers;

public interface IProductHandler
{
    ProductOutput Create(ProductInput productInput);
    ProductOutput Update(ProductInput productInput);
    void Delete(int productId);
    ProductOutput GetById(int product);
}
