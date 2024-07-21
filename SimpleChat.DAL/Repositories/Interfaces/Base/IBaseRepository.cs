using Ardalis.Specification;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SimpleChat.DAL.Repositories.Interfaces.Base;

public interface IBaseRepository<T>
    where T : class
{
    T Create(T entity);

    Task<T> CreateAsync(T entity);

    Task CreateRangeAsync(IEnumerable<T> items);

    EntityEntry<T> Update(T entity);

    public void UpdateRange(IEnumerable<T> items);

    void Delete(T entity);

    void DeleteRange(IEnumerable<T> items);
    Task<IEnumerable<T>> GetAllAsync(params ISpecification<T>[] specs);
    Task<T?> GetSingleOrDefaulAsync(params ISpecification<T>[] specs);
    Task<T?> GetFirstOrDefaulAsync(params ISpecification<T>[] specs);
    Task<int> SaveChangesAsync();
}
