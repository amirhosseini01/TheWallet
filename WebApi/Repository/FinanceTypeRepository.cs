using WebApi.Data;
using WebApi.Models;

namespace WebApi.Repository;

public class FinanceTypeRepository : GenericRepository<FinanceType>
{
    public FinanceTypeRepository(WebApiContext context) : base(context)
    {

    }
}