using FaustWeb.Domain.DTO.Filter;
using FaustWeb.Domain.DTO.Pagination;
using System.Linq.Expressions;

namespace FaustWeb.Application.Services.BaseService;

public interface IBaseService<T> where T : class
{
    IQueryable<T> GetAll();
    Task<T> GetByIdAsync(int id);
    Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
    Task<T> AddAsync(T entity);
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
    Task UpdateAsync(T entity);
    Task UpdateRangeAsync(IEnumerable<T> entities);
    Task DeleteAsync(int id);
    Task DeleteRangeAsync(IEnumerable<T> entities);

    Task<ResponsePaginationDto<T>> GetPaginated(IQueryable<T> query, RequestPaginationDto pagination);
    IQueryable<T> GetFilteredQuery(IQueryable<T> query, RequestFilterDto<T> filterDto);
}
