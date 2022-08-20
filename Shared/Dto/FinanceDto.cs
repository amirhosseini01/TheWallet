using System.ComponentModel.DataAnnotations;

namespace Shared.Dto;
public class FinanceDto
{

}
public class FinanceInputDto
{
    [Range(1, int.MaxValue)]
    public int? Id { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int FinanceTypeId { get; set; }

    [Required]
    [Range(int.MinValue, int.MaxValue)]
    public int? Amount { get; set; }

    [Required]
    public DateTime? PayDate { get; set; }

    [StringLength(500)]
    public string Description { get; set; }
}

public class FinancePaginationDto
{
    public int PageIndex { get; set; }
    public int TotalPages { get; set; }
    public List<FinanceListDto> List { get; set; }
}
public class FinanceListDto
{
    public int Id { get; set; }
    public string FinanceTypeTitle { get; set; }
    public int Amount { get; set; }
    public DateTime PayDate { get; set; }
    public string Description { get; set; }
}
public class FinanceListFilterDto
{
    public int Skip { get; set; } = 1;
    public int Take { get; set; } = 10;
}