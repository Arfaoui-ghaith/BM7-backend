using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BM7_Backend.Models;

public class User
{
    [Key, Required]
    public Guid id { get; set; } = Guid.NewGuid();
    [Required]
    public String name { get; set; }
    [Required]
    public String email { get; set; }
    public String password { get; set; }
    public byte[] passwordHash { get; set; }
    public byte[] passwordSalt { get; set; }

    [DefaultValue(Models.Role.Client)]
    public Role role { get; set; }
    public DateTime createdAt { get; set; } = DateTime.UtcNow;
    public DateTime updatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Category> categories { get; set; }
}