using System.ComponentModel.DataAnnotations;

namespace BM7_Backend.Models;

public class Category
{
    [Key, Required]
    public Guid id { get; set; } = Guid.NewGuid();
    [Required]
    public String title { get; set; }
    [Required]
    public String image { get; set; }
    public DateTime createdAt { get; set; } = DateTime.UtcNow;
    public DateTime updatedAt { get; set; } = DateTime.UtcNow;
    public User user { get; set; }
    public ICollection<Transaction> transactions { get; set; }
}