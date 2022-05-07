using WebApi.Dto;

namespace WebApi.Repository;
public interface IGenericRepository<T>
{
    Task Add(T entity);
    Task<ResponsePayload> Save();
}