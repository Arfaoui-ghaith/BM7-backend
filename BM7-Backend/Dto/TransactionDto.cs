using BM7_Backend.Models;
using Type = System.Type;

namespace BM7_Backend.Dto;

public class TransactionDto
{
    public float amount { get; set; }
    public String title { get; set; }
    public Type type { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
}