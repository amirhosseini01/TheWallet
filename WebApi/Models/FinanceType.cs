using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;
public class FinanceType : BaseModel
{
    [Required]
    [StringLength(250)]
    public string Title { get; set; }

    public virtual ICollection<Finance> Finances { get; set; }
}