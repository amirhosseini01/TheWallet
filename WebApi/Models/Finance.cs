using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;
public class Finance : BaseModel
{
    [Required]
    public string Type { get; set; }
    public int Amount { get; set; }
    public DateTime PayDate { get; set; }
    public string Description { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
}