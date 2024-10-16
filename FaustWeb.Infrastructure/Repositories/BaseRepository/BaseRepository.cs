using FaustWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FaustWeb.Infrastructure.Repositories.BaseRepository;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> dbSet;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        dbSet = _context.Set<T>();
    }

    public IQueryable<T> GetAll()
    {
        return dbSet
            .AsQueryable()
            .AsNoTracking();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await dbSet
             .AsQueryable()
             .AsNoTracking()
             .SingleOrDefaultAsync(x => x.Id == id)
             ?? throw new NullReferenceException($"{typeof(T).Name} not found.");

        return entity;
    }

    public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
    {
        var query = dbSet
            .AsQueryable()
            .AsNoTracking();

        foreach (var include in includes)
            query = query.Include(include);

        var entity = await query.SingleOrDefaultAsync(x => x.Id == id) ?? throw new NullReferenceException($"{typeof(T).Name} not found.");

        return entity;
    }

    public async Task<T> AddAsync(T entity)
    {
        var date = DateTime.Now;
        entity.CreatedDate = date;
        entity.UpdatedDate = date;

        dbSet.Add(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
    {
        var date = DateTime.Now;
        foreach (var entity in entities)
        {
            entity.CreatedDate = date;
            entity.UpdatedDate = date;
        }

        dbSet.AddRange(entities);
        await _context.SaveChangesAsync();

        return entities;
    }

    public async Task UpdateAsync(T entity)
    {
        entity.UpdatedDate = DateTime.Now;

        dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRangeAsync(IEnumerable<T> entities)
    {
        var date = DateTime.Now;
        foreach (var entity in entities)
            entity.UpdatedDate = date;

        dbSet.UpdateRange(entities);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);

        dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRangeAsync(IEnumerable<T> entities)
    {
        _context.RemoveRange(entities);
        await _context.SaveChangesAsync();
    }
}
