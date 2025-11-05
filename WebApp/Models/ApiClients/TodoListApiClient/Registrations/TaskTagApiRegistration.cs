using WebApi.Model.Entities.TodoDb;

namespace WebApp.Models.ApiClients.TodoListApiClient.Registrations;

public class TaskTagApiRegistration : IApiClientRegistration
{
    public void Register(ApiClientRegistry registry, HttpClient http)
    {
        registry.AddApiClient(new ApiClient<TaskTag>(http, "api/tasktagentity"));
    }
}
