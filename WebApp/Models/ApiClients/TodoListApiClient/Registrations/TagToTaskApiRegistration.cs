using WebApi.Entities.TodoDb;

namespace WebApp.Models.ApiClients.TodoListApiClient.Registrations;

public class TagToTaskApiRegistration : IApiClientRegistration
{
    public void Register(ApiClientRegistry registry, HttpClient http)
    {
        registry.AddApiClient(new ApiClient<TagToTask>(http, "api/tagtotaskentity"));
    }
}
