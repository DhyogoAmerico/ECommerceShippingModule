namespace ECommerceShippingModule.Domain.Interfaces.Repositories;

public interface IRepository<T>
{
    T? GetByIdAsync(Guid id);
    void AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}
