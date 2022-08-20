using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Dto;
using WebApi.Helpers;
using WebApi.Repository;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FinanceTypeController : ControllerBase
{
    private readonly FinanceTypeRepository _financeTypeRepository;

    public FinanceTypeController(FinanceTypeRepository financeTypeRepository)
    {
        _financeTypeRepository = financeTypeRepository;
    }
    [HttpPost("List")]
    public async Task<ResponsePayload<List<FinanceTypeListDto>>> List(PaginationDto dto)
    {
        if (!ModelState.IsValid)
        {
            return new ResponsePayload<List<FinanceTypeListDto>>(false, Message.InvalidData, null);
        }

         var result =  await PaginatedList<FinanceTypeListDto>.CreateAsync(
            _financeTypeRepository.GetQuery().AsNoTracking()
            .Select(x=> new FinanceTypeListDto()
            {
                Id = x.Id,
                Title = x.Title
            }), dto.Skip, dto.Take);

        return new ResponsePayload<List<FinanceTypeListDto>>(true, Message.SuccessfulyLoaded, result);
    }
}