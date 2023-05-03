using BM7_Backend.Models;

namespace BM7_Backend.Dto;

public class CategoryDto
{
    public Guid id { get; set; } = Guid.NewGuid();
    public String title { get; set; }
    public String image { get; set; }
    public DateTime createdAt { get; set; } = DateTime.UtcNow;
    public DateTime updatedAt { get; set; } = DateTime.UtcNow;
    public User user { get; set; }
    public ICollection<Transaction> transactions { get; set; }
}