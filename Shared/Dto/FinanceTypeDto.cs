using System.ComponentModel.DataAnnotations;

namespace Shared.Dto;
public class FinanceTypeDto
{
}
public class FinanceTypeListDto
{
    public int Id { get; set; }
    public string Title { get; set; }
}

public class FinanceTypeInputDto
{
    [Range(1, int.MaxValue)]
    public int? Id { get; set; }

    [Required]
    [StringLength(maximumLength: 250, MinimumLength = 1)]
    public string Title { get; set; }
}