using BM7_Backend.Models;
using Type = System.Type;

namespace BM7_Backend.Dto;

public class TransactionDto
{
    public Guid id { get; set; }
    public float amount { get; set; }
    public String title { get; set; }
    public bool status { get; set; }
    public DateTime createdAt { get; set; }
}