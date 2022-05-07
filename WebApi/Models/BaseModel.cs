using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class BaseModel
{
    [Key]
    public int Id { get; set; }
}