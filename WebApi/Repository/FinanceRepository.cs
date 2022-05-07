using WebApi.Data;
using WebApi.Models;

namespace WebApi.Repository;

public class FinanceRepository : GenericRepository<Finance>, IFinanceRepository
{
    public FinanceRepository(WebApiContext context) : base(context)
    {

    }
}