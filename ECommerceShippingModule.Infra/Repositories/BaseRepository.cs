using ECommerceShippingModule.Domain.Interfaces.Entities;
using ECommerceShippingModule.Domain.Interfaces.Repositories;

namespace ECommerceShippingModule.Infra.Repositories;

public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    protected HashSet<TEntity> Entities { get; set; }

    public void AddAsync(TEntity entity)
    {
        Entities.Add(entity);
    }

    public void Delete(TEntity entity)
    {
        Entities.Remove(entity);
    }

    public TEntity? GetByIdAsync(Guid id)
    {
        return Entities.FirstOrDefault(o => o.Id == id);
    }

    public void Update(TEntity entity)
    {
        Entities.RemoveWhere(o => o.Id == entity.Id);
        Entities.Add(entity);
    }
}
