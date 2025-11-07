using ECommerceShippingModule.Application.Inputs;
using ECommerceShippingModule.Application.Outputs;

namespace ECommerceShippingModule.Application.Intefaces.Handlers;

public interface IClientHandler
{
    ClientOutput Create(ClientInput clientInput);
    ClientOutput Update(ClientInput clientInput);
    void Delete(int clientId);
    ClientOutput GetById(int clientId);
}
