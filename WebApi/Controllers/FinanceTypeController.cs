using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Dto;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Repository;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FinanceTypeController : ControllerBase
{
    private readonly FinanceTypeRepository _financeTypeRepository;
    private readonly IMapper _mapper;

    public FinanceTypeController(FinanceTypeRepository financeTypeRepository,
     IMapper mapper)
    {
        _financeTypeRepository = financeTypeRepository;
        _mapper = mapper;
    }
    [HttpPost("List")]
    public async Task<ResponsePayload<List<FinanceTypeListDto>>> List(PaginationDto dto)
    {
        if (!ModelState.IsValid)
        {
            return new ResponsePayload<List<FinanceTypeListDto>>(false, Message.InvalidData, null);
        }

        var result = await PaginatedList<FinanceTypeListDto>.CreateAsync(
           _financeTypeRepository.GetQuery().AsNoTracking()
           .Select(x => new FinanceTypeListDto()
           {
               Id = x.Id,
               Title = x.Title
           }), dto.Skip, dto.Take);

        return new ResponsePayload<List<FinanceTypeListDto>>(true, Message.SuccessfulyLoaded, result);
    }

    [HttpPost]
    public async Task<ResponsePayload> Post(FinanceTypeInputDto inputDto)
    {
        if (!ModelState.IsValid)
        {
            return new ResponsePayload(false, Message.InvalidData);
        }

        var entity = _mapper.Map<FinanceType>(inputDto);

        if (entity.Id <= 0)
        {
            await _financeTypeRepository.Add(entity);
        }
        else
        {
            _financeTypeRepository.Update(entity);
        }

        return await _financeTypeRepository.Save();
    }
}