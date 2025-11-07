using ECommerceShippingModule.Domain.Entities;
using ECommerceShippingModule.Domain.Interfaces.Repositories;
using ECommerceShippingModule.Infra.DataContext;

namespace ECommerceShippingModule.Infra.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(Context context)
    {
        Entities = context.Products;
    }
}
