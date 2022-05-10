using System.ComponentModel.DataAnnotations;

namespace Site.Dto;
public class FinanceDto
{

}
public class FinanceInputDto
{
    [Range(1, int.MaxValue)]
    public int? Id { get; set; }

    [Required]
    [StringLength(maximumLength: 250, MinimumLength = 1)]
    public string Type { get; set; }

    [Required]
    [Range(int.MinValue, int.MaxValue)]
    public int? Amount { get; set; }

    [Required]
    public DateTime? PayDate { get; set; }

    [StringLength(500)]
    public string Description { get; set; }
}

public class FinanceListDto
{
    public int Id { get; set; }
    public string Type { get; set; }
    public int Amount { get; set; }
    public DateTime PayDate { get; set; }
    public string Description { get; set; }
}
public class FinanceListFilterDto
{
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 10;
}