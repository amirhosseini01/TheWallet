using Microsoft.EntityFrameworkCore;
using Shared.Dto;
using WebApi.Data;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Repository;
public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
{
    private readonly WebApiContext _context;
    private readonly DbSet<T> _dbSet;
    public GenericRepository(WebApiContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
    public async Task Add(T entity)
    {
        await _dbSet.AddAsync(entity);
    }
    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public async Task<T> GetById(int id)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    public IQueryable<T> GetQuery()
    {
        return _dbSet;
    }

    public async Task<ResponsePayload> Save()
    {
        try
        {
            await _context.SaveChangesAsync();
            return new ResponsePayload(true, Message.SuccessSaved);
        }
        catch (Exception ex)
        {
            //todo: log this error message
            string errMessage = $"{ex.Message} {ex.InnerException?.Message}";
            return new ResponsePayload(true, Message.SuccessSaved);
        }
    }
}