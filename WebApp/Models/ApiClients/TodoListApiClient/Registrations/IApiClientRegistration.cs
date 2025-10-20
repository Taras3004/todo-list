namespace WebApp.Models.ApiClients.TodoListApiClient.Registrations;

public interface IApiClientRegistration
{
    void Register(ApiClientRegistry registry, HttpClient http);
}
