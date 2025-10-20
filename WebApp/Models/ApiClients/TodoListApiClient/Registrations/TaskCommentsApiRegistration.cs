using WebApi.Entities.TodoDb;

namespace WebApp.Models.ApiClients.TodoListApiClient.Registrations;

public class TaskCommentsApiRegistration : IApiClientRegistration
{
    public void Register(ApiClientRegistry registry, HttpClient http)
    {
        registry.AddApiClient(new ApiClient<TaskComment>(http, "api/taskcommententity"));
    }
}
