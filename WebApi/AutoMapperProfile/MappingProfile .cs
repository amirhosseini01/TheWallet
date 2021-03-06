using AutoMapper;
using Shared.Dto;
using WebApi.Models;

namespace WebApi.AutoMapperProfile;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Finance
        CreateMap<Finance, FinanceInputDto>();
        CreateMap<FinanceInputDto, Finance>();
    }
}