using WebApi.Entities.TodoDb;
using WebApp.Models.ApiClients.TodoListApiClient.CustomApiClients;

namespace WebApp.Models.ApiClients.TodoListApiClient.Registrations;

public class TodoTaskApiRegistration : IApiClientRegistration
{
    public void Register(ApiClientRegistry registry, HttpClient http)
    {
        var pageApi = registry.GetApiClient<TodoTaskPage>();
        var taskApi = new TodoTaskApiClient(http, "api/todotaskentity", pageApi);
        registry.AddApiClient(taskApi);
    }
}
