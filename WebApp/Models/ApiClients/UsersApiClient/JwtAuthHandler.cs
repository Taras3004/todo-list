using System.Net.Http.Headers;

namespace WebApp.Models.ApiClients.UsersApiClient;

public class JwtAuthHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public JwtAuthHandler(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = this.httpContextAccessor.HttpContext?.Request.Cookies["jwt"];

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return base.SendAsync(request, cancellationToken);
    }
}
