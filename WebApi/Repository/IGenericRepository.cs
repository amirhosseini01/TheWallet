using Shared.Dto;

namespace WebApi.Repository;
public interface IGenericRepository<T>
{
    Task<T> GetById(int id);
    IQueryable<T> GetQuery();
    Task Add(T entity);
    void Update(T entity);
    Task<ResponsePayload> Save();
}