using Shared.Dto;

namespace WebApi.Repository;
public interface IGenericRepository<T>
{
    IQueryable<T> GetQuery();
    Task Add(T entity);
    Task<ResponsePayload> Save();
}