using ECommerceShippingModule.Domain.Entities;
using ECommerceShippingModule.Domain.Interfaces.Repositories;
using ECommerceShippingModule.Infra.DataContext;

namespace ECommerceShippingModule.Infra.Repositories;

public class ClientRepository : BaseRepository<Client>, IClientRepository
{
    public ClientRepository(Context context)
    {
        Entities = context.Clients;
    }
}
