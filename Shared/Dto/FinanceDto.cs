using System.ComponentModel.DataAnnotations;

namespace Shared.Dto;
public class FinanceDto
{

}
public class FinanceInputDto
{
    [Range(1, long.MaxValue)]
    public int? Id { get; set; }

    [Required]
    [Range(1, long.MaxValue)]
    public int FinanceTypeId { get; set; }

    [Required]
    [Range(long.MinValue, long.MaxValue)]
    public long? Amount { get; set; }

    [Required]
    public DateTime? PayDate { get; set; }

    [StringLength(500)]
    public string Description { get; set; }
}

public class FinancePaginationDto
{
    public int PageIndex { get; set; }
    public int TotalPages { get; set; }
    public long WalletBalance { get; set; }
    public List<FinanceListDto> List { get; set; }
}
public class FinanceListDto
{
    public int Id { get; set; }
    public string FinanceTypeTitle { get; set; }
    public long Amount { get; set; }
    public DateTime PayDate { get; set; }
    public string Description { get; set; }
}

public class FinanceApiFilterDto : PaginationDto
{
    public int? TypeId { get; set; }
    public string SearchValue { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? UntilDate { get; set; }
    public long? FromAmount { get; set; }
    public long? UntilAmount { get; set; }
}