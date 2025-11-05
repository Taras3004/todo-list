using WebApi.Model.Entities.TodoDb;

namespace WebApp.Models.ApiClients.TodoListApiClient.Registrations;

public class TodoTaskPageApiRegistration : IApiClientRegistration
{
    public void Register(ApiClientRegistry registry, HttpClient http)
    {
        registry.AddApiClient(new ApiClient<TodoTaskPage>(http, "api/taskpageentity"));
    }
}
