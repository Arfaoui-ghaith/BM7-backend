using System.ComponentModel.DataAnnotations;

namespace BM7_Backend.Models;

public class Transaction
{
    [Key, Required]
    public Guid id { get; set; } = Guid.NewGuid();
    [Required]
    public float amount { get; set; }
    [Required]
    public String title { get; set; }
    [Required]
    public bool status { get; set; }
    public DateTime createdAt { get; set; } = DateTime.UtcNow;
    public DateTime updatedAt { get; set; } = DateTime.UtcNow;
    public Category category { get; set; }
}