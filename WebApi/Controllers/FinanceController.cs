using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;
using WebApi.Repository;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FinanceController : ControllerBase
{
    private readonly IFinanceRepository _financeRepository;

    public FinanceController(IFinanceRepository financeRepository)
    {
        _financeRepository = financeRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Post(FinanceInputDto inputDto)
    {
       return Ok();
    }
}
