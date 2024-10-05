using FaustWeb.Domain.DTO.Filter;
using FaustWeb.Domain.DTO.Pagination;
using FaustWeb.Infrastructure.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FaustWeb.Application.BaseService;

public class BaseService<T>(IBaseRepository<T> baseRepository) : IBaseService<T> where T : class
{
    public IQueryable<T> GetAll()
    {
        return baseRepository.GetAll();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await baseRepository.GetByIdAsync(id);
    }

    public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
    {
        return await baseRepository.GetByIdAsync(id, includes);
    }

    public async Task<T> AddAsync(T entity)
    {
        return await baseRepository.AddAsync(entity);
    }

    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entity)
    {
        return await baseRepository.AddRangeAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        await baseRepository.UpdateAsync(entity);
    }

    public async Task UpdateRangeAsync(IEnumerable<T> entities)
    {
        await baseRepository.UpdateRangeAsync(entities);
    }

    public async Task DeleteAsync(int id)
    {
        await baseRepository.DeleteAsync(id);
    }

    public async Task DeleteRangeAsync(IEnumerable<T> entities)
    {
        await baseRepository.DeleteRangeAsync(entities);
    }


    public IQueryable<T> GetFilteredQuery(IQueryable<T> query, RequestFilterDto<T> filterDto)
    {
        if (filterDto.SearchTerms != null && filterDto.SearchTerms.Any())
        {
            foreach (var searchTerm in filterDto.SearchTerms)
                query = query.Where(searchTerm);
        }

        if (filterDto.OrderBy != null)
        {
            query = filterDto.IsAsc
                ? query.OrderBy(filterDto.OrderBy)
                : query.OrderByDescending(filterDto.OrderBy);
        }

        return query;
    }

    public async Task<ResponsePaginationDto<T>> GetPaginated(IQueryable<T> query, RequestPaginationDto pagination)
    {
        var totalCount = await query.CountAsync();
        var data = await query
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();

        return new ResponsePaginationDto<T>
        {
            Data = data,
            PageNumber = pagination.PageNumber,
            PageSize = pagination.PageSize,
            TotalPages = (int)Math.Ceiling((double)totalCount / pagination.PageSize),
            HasNextPage = pagination.PageNumber * pagination.PageSize < totalCount,
            HasPreviousPage = pagination.PageNumber > 1
        };
    }
}
