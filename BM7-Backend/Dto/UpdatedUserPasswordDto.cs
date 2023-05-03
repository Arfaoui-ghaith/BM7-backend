using System.Security.Cryptography;
using System.Text;

namespace BM7_Backend.Dto;

public class UpdatedUserPasswordDto
{

    public String current_password { get; set; }
    public String password { get; set; }
    public String confirmPassword { get; set; }

    public void hash()
    {
        var sha = SHA256.Create();
        var asByteArray1 = Encoding.Default.GetBytes(this.current_password);
        var asByteArray2 = Encoding.Default.GetBytes(this.password);
        var asByteArray3 = Encoding.Default.GetBytes(this.confirmPassword);
        
        var hashedCurrent_password = sha.ComputeHash(asByteArray1);
        var hashedPassword = sha.ComputeHash(asByteArray2);
        var hashedConfirmPassword = sha.ComputeHash(asByteArray3);
        
        this.current_password = Convert.ToBase64String(hashedCurrent_password);
        this.password = Convert.ToBase64String(hashedPassword);
        this.confirmPassword = Convert.ToBase64String(hashedConfirmPassword);
    }
}