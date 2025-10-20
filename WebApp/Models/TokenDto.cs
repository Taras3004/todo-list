namespace WebApp.Models;

public class TokenDto
{
    public TokenDto(string token)
    {
        this.Token = token;
    }

    public string Token { get; init; }
}
