using BM7_Backend.Models;

namespace BM7_Backend.Dto;

public class UserDto
{
    public Guid id { get; set; }
    public String name { get; set; } 
    public String email { get; set; }
    public DateTime createdAt { get; set; }
}