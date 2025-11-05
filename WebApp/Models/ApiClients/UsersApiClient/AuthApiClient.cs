using WebApp.Models.ViewModels;

namespace WebApp.Models.ApiClients.UsersApiClient;

public class AuthApiClient
{
    private readonly HttpClient httpClient;

    public AuthApiClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<TokenDto?> LoginAsync(LoginViewModel loginModel)
    {
        var response = await this.httpClient.PostAsJsonAsync("/api/auth/login", loginModel);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<TokenDto>();
        }

        return null;
    }

    public async Task<bool> RegisterAsync(RegisterViewModel registerModel)
    {
        var response = await this.httpClient.PostAsJsonAsync("/api/auth/register", registerModel);
        return response.IsSuccessStatusCode;
    }
}
