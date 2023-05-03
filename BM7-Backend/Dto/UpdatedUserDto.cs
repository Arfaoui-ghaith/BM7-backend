using System.Security.Cryptography;
using System.Text;

namespace BM7_Backend.Dto;

public class UpdatedUserDto
{
    public String name { get; set; }
    public String email { get; set; }
}