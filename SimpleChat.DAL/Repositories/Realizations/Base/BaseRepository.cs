using Ardalis.Specification.EntityFrameworkCore;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SimpleChat.DAL.Persistence;
using SimpleChat.DAL.Repositories.Interfaces.Base;

namespace SimpleChat.DAL.Repositories.Realizations.Base;

public abstract class BaseRepository<T> : IBaseRepository<T>
    where T : class
{
    private ChatDbContext _dbContext = null!;

    protected BaseRepository(ChatDbContext context)
    {
        _dbContext = context;
    }

    protected BaseRepository()
    {
    }
    public ChatDbContext DbContext { init => _dbContext = value; }
    public async Task<IEnumerable<T>> GetAllAsync(params ISpecification<T>[] specs)
    {
        return await ApplySpecifications(specs)
            .ToListAsync();
    }

    public T Create(T entity)
    {
        return _dbContext.Set<T>().Add(entity).Entity;
    }

    public async Task<T> CreateAsync(T entity)
    {
        var tmp = await _dbContext.Set<T>().AddAsync(entity);
        return tmp.Entity;
    }

    public Task CreateRangeAsync(IEnumerable<T> items)
    {
        return _dbContext.Set<T>().AddRangeAsync(items);
    }

    public EntityEntry<T> Update(T entity)
    {
        return _dbContext.Set<T>().Update(entity);
    }

    public void UpdateRange(IEnumerable<T> items)
    {
        _dbContext.Set<T>().UpdateRange(items);
    }

    public void Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> items)
    {
        _dbContext.Set<T>().RemoveRange(items);
    }

    public async Task<T?> GetSingleOrDefaulAsync(params ISpecification<T>[] specs)
    {
        return await ApplySpecifications(specs)
           .SingleOrDefaultAsync();
    }

    public async Task<T?> GetFirstOrDefaulAsync(params ISpecification<T>[] specs)
    {
        return await ApplySpecifications(specs)
            .FirstOrDefaultAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    private IQueryable<T> ApplySpecifications(params ISpecification<T>[] specs)
    {
        var query = _dbContext.Set<T>().AsQueryable();

        foreach (var spec in specs)
        {
            query = SpecificationEvaluator.Default.GetQuery(query, spec);
        }

        return query;
    }
}
