using System.ComponentModel.DataAnnotations;

namespace TheWallet.Models;

public class Finance
{
    public string Type { get; set; }
    public long Amount { get; set; }
    public string Description { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
}