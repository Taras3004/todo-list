using WebApi.Model.Entities.TodoDb;

namespace WebApp.Models.ApiClients.TodoListApiClient.CustomApiClients;

public class TodoListApiClient : ApiClient<TodoList>
{
    private readonly ApiClient<TodoTask> todoTaskApiClient;

    public TodoListApiClient(HttpClient http, string apiRoute, ApiClient<TodoTask> todoTaskApiClient) : base(http, apiRoute)
    {
        this.todoTaskApiClient = todoTaskApiClient;
    }

    public override async Task DeleteAsync(int id)
    {
        var pages = this.todoTaskApiClient.GetAllAsync().Result!.Where(x => x.TodoListId == id);

        foreach (var page in pages)
        {
            await this.todoTaskApiClient.DeleteAsync(page.Id);
        }

        await base.DeleteAsync(id);
    }
}
