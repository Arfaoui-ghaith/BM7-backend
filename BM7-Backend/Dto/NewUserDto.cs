using System.Security.Cryptography;
using System.Text;

namespace BM7_Backend.Dto;

public class NewUserDto
{
    public String name { get; set; } 
    public String email { get; set; }
    public String password { get; set; }
    public String confirmPassword { get; set; }
    
    public void hash()
    {
        var sha = SHA256.Create();
        var asByteArray = Encoding.Default.GetBytes(this.password);
        var hashedPassword = sha.ComputeHash(asByteArray);
        this.password = Convert.ToBase64String(hashedPassword);
    }
}