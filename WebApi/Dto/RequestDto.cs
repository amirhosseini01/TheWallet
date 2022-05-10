using System.ComponentModel.DataAnnotations;

namespace WebApi.Dto;

public class PaginationDto
{
    [Range(0 , int.MaxValue)]
    public int Skip { get; set; } = 0;
    [Range(1 , int.MaxValue)]
    public int Take { get; set; } = 10;
}