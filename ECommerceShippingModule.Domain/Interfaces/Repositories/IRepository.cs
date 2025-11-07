namespace ECommerceShippingModule.Domain.Interfaces.Repositories;

public interface IRepository<T>
{
    T GetById(Guid id);
    IList<T> GetAll();
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}
