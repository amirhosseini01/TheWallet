using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Dto;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Repository;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FinanceController : ControllerBase
{
    private readonly IFinanceRepository _financeRepository;
    private readonly IMapper _mapper;

    public FinanceController(IFinanceRepository financeRepository, IMapper mapper)
    {
        _financeRepository = financeRepository;
        _mapper = mapper;
    }
    [HttpPost("List")]
    public async Task<IActionResult> List(PaginationDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResponsePayload(false, Message.InvalidData));
        }

        int count = await _financeRepository.GetQuery().CountAsync();
        FinancePaginationDto result = new()
        {
            PageIndex = dto.Skip,
            TotalPages = (int)Math.Ceiling(count / (double)dto.Take),
            List = await _financeRepository.GetQuery().Select(x => new FinanceListDto()
            {
                Id = x.Id,
                Amount = x.Amount,
                Type = x.Type,
                PayDate = x.PayDate,
                Description = x.Description
            }).OrderByDescending(x => x.Id).Skip((dto.Skip - 1) * dto.Take).Take(dto.Take).ToListAsync()
        };

        return Ok(new ResponsePayload<FinancePaginationDto>(true, Message.SuccessfulyLoaded, result));
    }

    [HttpPost]
    public async Task<IActionResult> Post(FinanceInputDto inputDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResponsePayload(false, Message.InvalidData));
        }

        var entity = _mapper.Map<Finance>(inputDto);

        entity.CreateDate = DateTime.Now;
        entity.UpdateDate = DateTime.Now;

        await _financeRepository.Add(entity);
        var res = await _financeRepository.Save();

        if (!res.Succeeded)
        {
            return BadRequest(res);
        }

        return Ok(res);
    }
}
