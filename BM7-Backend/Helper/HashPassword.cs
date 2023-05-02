using System.Security.Cryptography;
using System.Text;

namespace BM7_Backend.Helper;

public class HashPassword
{
    public HashPassword()
    {
    }
    
    public String hash(String password)
    {
        var sha = SHA256.Create();
        var asByteArray = Encoding.Default.GetBytes(password);
        var hashedPassword = sha.ComputeHash(asByteArray);
        return Convert.ToBase64String(hashedPassword);
    }
}