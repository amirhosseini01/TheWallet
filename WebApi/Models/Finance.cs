using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;
public class Finance : BaseModel
{
    [StringLength(250)]
    public int FinanceTypeId { get; set; }
    public long Amount { get; set; }
    public DateTime PayDate { get; set; }
    [StringLength(500)]
    public string Description { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }

    public virtual FinanceType FinanceType { get; set; }
}