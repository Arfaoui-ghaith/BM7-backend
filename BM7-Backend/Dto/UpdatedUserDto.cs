using System.Security.Cryptography;
using System.Text;
using BM7_Backend.Models;

namespace BM7_Backend.Dto;

public class UpdatedUserDto
{
    public String name { get; set; }
    public String email { get; set; }
    public Role role { get; set; }
}