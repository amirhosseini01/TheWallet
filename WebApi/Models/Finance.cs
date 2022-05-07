using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;
public class Finance : BaseModel
{
    [Required]
    [StringLength(250)]
    public string Type { get; set; }
    public int Amount { get; set; }
    public DateTime PayDate { get; set; }
    [StringLength(500)]
    public string Description { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
}