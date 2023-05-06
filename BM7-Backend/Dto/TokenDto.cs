namespace BM7_Backend.Dto;

public class TokenDto
{
    public String token { get; set; }

    public TokenDto(string token)
    {
        this.token = token;
    }
}