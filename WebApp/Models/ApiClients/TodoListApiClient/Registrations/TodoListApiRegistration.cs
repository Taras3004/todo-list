using WebApi.Entities.TodoDb;

namespace WebApp.Models.ApiClients.TodoListApiClient.Registrations;

public class TodoListApiRegistration : IApiClientRegistration
{
    public void Register(ApiClientRegistry registry, HttpClient http)
    {
        var todoTaskApi = registry.GetApiClient<TodoTask>();
        var todoListApi = new CustomApiClients.TodoListApiClient(http, "api/todolistentity", todoTaskApi);

        registry.AddApiClient(todoListApi);
    }
}
