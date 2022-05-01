using System.ComponentModel.DataAnnotations;

namespace TheWallet.Models;

public class Finance
{
    [Key]
    public int Id { get; set; }
    [Required]
    public long Amount { get; set; }
    [StringLength(500)]
    public string? Description { get; set; }
    [Required]
    public DateTime CreateDate { get; set; }
    [Required]
    public DateTime UpdateDate { get; set; }
}