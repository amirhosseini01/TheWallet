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
    public async Task<ResponsePayload<FinancePaginationDto>> List(FinanceApiFilterDto dto)
    {
        if (!ModelState.IsValid)
        {
            return new ResponsePayload<FinancePaginationDto>(false, Message.InvalidData, null);
        }
        var query = _financeRepository.GetQuery().AsNoTracking();

        if (dto.FromDate is not null)
        {
            query = query.Where(x => x.PayDate >= dto.FromDate);
        }
        if (dto.UntilDate is not null)
        {
            query = query.Where(x => x.PayDate <= dto.UntilDate);
        }

        if (dto.FromAmount is not null)
        {
            query = query.Where(x => x.Amount >= dto.FromAmount);
        }
        if (dto.UntilAmount is not null)
        {
            query = query.Where(x => x.Amount <= dto.UntilAmount);
        }

        if (!string.IsNullOrEmpty(dto.SearchValue))
        {
            query = query.Where(x => x.Description.Contains(dto.SearchValue) ||
            x.FinanceType.Title.Contains(dto.SearchValue) ||
            x.Id.ToString().Contains(dto.SearchValue));
        }

        int count = await query.CountAsync();
        FinancePaginationDto result = new()
        {
            PageIndex = dto.Skip,
            TotalPages = (int)Math.Ceiling(count / (double)dto.Take),
            WalletBalance = await _financeRepository.GetQuery().AsNoTracking().SumAsync(xx => xx.Amount),
            List = await query.Select(x => new FinanceListDto()
            {
                Id = x.Id,
                Amount = x.Amount,
                FinanceTypeTitle = x.FinanceType.Title,
                PayDate = x.PayDate,
                Description = x.Description
            }).OrderByDescending(x => x.Id).Skip((dto.Skip - 1) * dto.Take).Take(dto.Take).ToListAsync(),
        };

        return new ResponsePayload<FinancePaginationDto>(true, Message.SuccessfulyLoaded, result);
    }

    [HttpGet("ById/{id}")]
    public async Task<ResponsePayload<FinanceInputDto>> ById(int id)
    {
        if (id <= 0)
        {
            return new ResponsePayload<FinanceInputDto>(false, Message.InvalidData, null);
        }

        var entity = await _financeRepository.GetById(id);
        if (entity is null)
        {
            return new ResponsePayload<FinanceInputDto>(false, Message.NotFound, null);
        }

        var dto = _mapper.Map<FinanceInputDto>(entity);

        return new ResponsePayload<FinanceInputDto>(true, Message.SuccessfulyLoaded, dto);
    }
    [HttpGet("Remove/{id}")]
    public async Task<ResponsePayload> Remove(int id)
    {
        if (id <= 0)
        {
            return new ResponsePayload(false, Message.InvalidData);
        }

        var entity = await _financeRepository.GetById(id);
        if (entity is null)
        {
            return new ResponsePayload(false, Message.NotFound);
        }

        _financeRepository.Remove(entity);
        var res = await _financeRepository.Save();

        return res;
    }

    [HttpPost]
    public async Task<ResponsePayload> Post(FinanceInputDto inputDto)
    {
        if (!ModelState.IsValid)
        {
            return new ResponsePayload(false, Message.InvalidData);
        }

        var entity = _mapper.Map<Finance>(inputDto);

        entity.UpdateDate = DateTime.Now;

        if (entity.Id <= 0)
        {
            entity.CreateDate = DateTime.Now;
            await _financeRepository.Add(entity);
        }
        else
        {
            _financeRepository.Update(entity);
        }

        var res = await _financeRepository.Save();

        return res;
    }
}
