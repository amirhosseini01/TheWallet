using System.ComponentModel.DataAnnotations;

namespace WebApi.Dto;

public class PaginationDto
{
    [Range(1 , int.MaxValue)]
    public int Skip { get; set; } = 1;
    [Range(1 , int.MaxValue)]
    public int Take { get; set; } = 10;
}